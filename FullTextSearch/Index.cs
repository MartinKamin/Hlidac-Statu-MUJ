﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace FullTextSearch
{
    public class Index<T> where T : IEquatable<T>
    {
        public TokenTree<T> SortedTokens { get; private set; } = new TokenTree<T>();

        private readonly ITokenizer _tokenizer;

        private readonly Options _options;

        public Index(IEnumerable<T> inputObjects)
            : this(inputObjects, Tokenizer.DefaultTokenizer(), Options.DefaultOptions())
        {
        }

        public Index(IEnumerable<T> inputObjects, ITokenizer tokenizer, Options options)
        {
            _tokenizer = tokenizer;
            _options = options;
            BuildIndex(inputObjects);
        }

        //ctor for deserialization        
        private Index(TokenTree<T> sortedTokens, ITokenizer tokenizer, Options options)
        {
            _tokenizer = Tokenizer.DefaultTokenizer();
            _options = Options.DefaultOptions();
            SortedTokens = sortedTokens;

            // foreach (var sentence in sentences)
            // {
            //     SortedTokens.AddTokens(sentence.Tokens);
            // }
        }

        public static Index<T> Deserialize(byte[] json, ITokenizer tokenizer = null, Options options = null)
        {
            //json to Sentence list
            tokenizer = tokenizer ?? Tokenizer.DefaultTokenizer();
            options = options ?? Options.DefaultOptions();
            
            List<SerializableSentence<T>> deserializedSentences =
                JsonSerializer.Deserialize<List<SerializableSentence<T>>>(json);

            if (deserializedSentences is null)
                throw new Exception("Deserialization failed");

            var tokenTree = new TokenTree<T>();
            
            foreach (var deserializedSentence in deserializedSentences)
            {
                var sentence = new Sentence<T>(deserializedSentence, tokenizer);
                tokenTree.AddTokens(sentence.Tokens);
            }
                
            return new Index<T>(tokenTree, tokenizer, options);
        }
        
        public byte[] Serialize()
        {
            return SortedTokens.Serialize();
        }

        private void BuildIndex(IEnumerable<T> inputObjects)
        {
            Devmasters.DT.StopWatchLaps swl = new Devmasters.DT.StopWatchLaps();
            var intv = swl.AddAndStartLap($"BuildIndex for {inputObjects.Count()} items");
            foreach (T inputObject in inputObjects)
            {
                if (inputObject == null)
                    continue;
                var sentence = new Sentence<T>(inputObject, _tokenizer);

                SortedTokens.AddTokens(sentence.Tokens);
            }

            intv.Stop();
            Log.Logger.Info(string.Join("\n", swl.Laps.Select(m => $"{m.Name} : {m.ExactElapsedMs:# ##0.00}ms")));
        }

        /// <summary>
        /// Searches Index for
        /// </summary>
        /// <param name="query">What is searched for</param>
        /// <param name="count">How many results gets back</param>
        /// <param name="sortFunctionDescending">Sorts results with the same weight descending</param>
        /// <param name="filterFunction">Reduce returned results</param>
        /// <returns></returns>
        public IEnumerable<Result<T>> Search(string query, int count, Func<T, int> sortFunctionDescending = null,
            Func<T, bool> filterFunction = null)
        {
            Devmasters.DT.StopWatchLaps swl = new Devmasters.DT.StopWatchLaps();
            var intv = swl.AddAndStartLap($"{query}: tokenize");
            var tokenizedQuery = _tokenizer.Tokenize(query);

            intv.Stop();
            intv = swl.AddAndStartLap($"{query}: sort tokens");

            var foundSentences = AndSearch(tokenizedQuery);

            if (filterFunction != null)
            {
                foundSentences = foundSentences
                    .Where(x => filterFunction(x.Original));
            }

            if (!foundSentences.Any())
                return Enumerable.Empty<Result<T>>();
            intv.Stop();

            intv = swl.AddAndStartLap($"{query}: calc score");


            var summedResults = ScoreSentences(foundSentences, tokenizedQuery);

            intv.Stop();
            intv = swl.AddAndStartLap($"{query}: final order");

            // zbaví se duplicit (synonym)
            var final = summedResults
                .GroupBy(sentence => sentence.Sentence.Original,
                    (key, scoredSentences) =>
                    {
                        var chosenResult = scoredSentences
                            .OrderByDescending(r => r.Score)
                            .FirstOrDefault();
                        return chosenResult;
                    })
                .OrderByDescending(x => x.Score);
            intv.Stop();

            // pokud existují priority, seřadí ještě pořadí výsledků podle priorit
            if (sortFunctionDescending != null)
            {
                intv = swl.AddAndStartLap($"{query}: after final resort descending");
                final = final.ThenByDescending(x => sortFunctionDescending(x.Sentence.Original));
                intv.Stop();
            }

            if (swl.Summary().ExactElapsedMs >= 500d)
            {
                Log.Logger.Info(
                    string.Join("\n", swl.Laps.Select(m => $"{m.Name} : {m.ExactElapsedMs:# ##0.00}ms"))
                    + $"\nTOTAL : {swl.Summary().ExactElapsedMs:# ##0.00}ms"
                );
            }

            return final
                .Take(count)
                .Select(x => new Result<T>()
                {
                    Original = x.Sentence.Original,
                    Score = x.Score
                });
        }

        //najde všechny věty, kde se tokeny vyskytují
        private IEnumerable<Sentence<T>> AndSearch(string[] tokenizedQuery)
        {
            IEnumerable<Sentence<T>> results = Enumerable.Empty<Sentence<T>>();
            if (tokenizedQuery.Length == 0)
                return results;

            IEnumerable<Sentence<T>> previousSentences = null;
            foreach (string queryToken in tokenizedQuery)
            {
                var foundTokens = SortedTokens.FindTokens(queryToken);
                var foundSentences = foundTokens.SelectMany(t => t.Sentences);

                if (previousSentences is null)
                {
                    previousSentences = foundSentences;
                    continue;
                }

                previousSentences = previousSentences.Intersect(foundSentences);
            }

            return previousSentences;
        }

        //oskóruje věty
        private List<ScoredSentence<T>> ScoreSentences(IEnumerable<Sentence<T>> foundSentences, string[] tokenizedQuery)
        {
            var result = new List<ScoredSentence<T>>();

            foreach (var sentence in foundSentences)
            {
                var score = ScoreSentence(sentence, tokenizedQuery);
                result.Add(new ScoredSentence<T>(sentence, score));
            }

            return result;
        }

        private Double ScoreSentence(Sentence<T> sentence, string[] tokenizedQuery)
        {
            if (tokenizedQuery.Length == 0)
                return 0;

            double score = 0;

            int firstWordBonusTokenPosition = 0;
            int chainBonusTokenPosition = 0;
            double chainScore = 0;
            HashSet<string> tokensToScore = new HashSet<string>(tokenizedQuery);

            for (int wordPosition = 0; wordPosition < sentence.Tokens.Count; wordPosition++)
            {
                // token score
                if (tokensToScore.Count > 0)
                    score += ScoreToken(sentence.Tokens[wordPosition], tokensToScore);

                // bonus for first words
                if (_options.FirstWordsBonus != null
                    && wordPosition < _options.FirstWordsBonus.BonusWordsCount
                    && firstWordBonusTokenPosition < tokenizedQuery.Length)
                {
                    string queryToken = tokenizedQuery[firstWordBonusTokenPosition];
                    if (sentence.Tokens[wordPosition].StartsWith(queryToken))
                    {
                        score += queryToken.Length
                                 * (_options.FirstWordsBonus.MaxBonusMultiplier -
                                    (_options.FirstWordsBonus.BonusMultiplierDegradation * wordPosition));

                        firstWordBonusTokenPosition++;
                    }
                }

                // bonus for longest word chain
                if (_options.ChainBonusMultiplier.HasValue
                    && chainBonusTokenPosition < tokenizedQuery.Length)
                {
                    string queryToken = tokenizedQuery[chainBonusTokenPosition];
                    if (sentence.Tokens[wordPosition].StartsWith(queryToken))
                    {
                        chainScore += queryToken.Length;
                        chainBonusTokenPosition++;
                    }
                    else if (chainScore > 0) // ends after missing match
                        break;

                    if (chainScore > 1)
                        score += chainScore * _options.ChainBonusMultiplier.Value; //todo: put it to options
                }
            }

            // Query == sentence
            if (sentence.Text == string.Join(" ", tokenizedQuery))
            {
                return score + _options.ExactMatchBonus ?? 0;
            }

            // sentence starts with query without its last word
            if (tokenizedQuery.Length > 2) // 3+ words
            {
                string shorterQuery = string.Join(" ", tokenizedQuery.Take(tokenizedQuery.Length - 1));
                if (sentence.Text.StartsWith(shorterQuery, StringComparison.Ordinal))
                {
                    return score + _options.AlmostExactMatchBonus ?? 0;
                }
            }

            return score;
        }

        private double ScoreToken(Token<T> token, HashSet<string> queryTokens)
        {
            double overallScore = 0;
            string hit = "";

            foreach (var queryToken in queryTokens)
            {
                if (token.StartsWith(queryToken))
                {
                    double basicScore = queryToken.Length;

                    // bonus for whole word
                    if (_options.WholeWordBonusMultiplier.HasValue
                        && queryToken.Length == token.Word.Length)
                    {
                        basicScore *= _options.WholeWordBonusMultiplier.Value;
                    }

                    overallScore += basicScore;
                    hit = queryToken;
                    break;
                }
            }

            //one word is only matched once!
            if (!string.IsNullOrWhiteSpace(hit))
                queryTokens.Remove(hit);

            return overallScore;
        }
    }
}