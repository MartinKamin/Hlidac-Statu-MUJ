namespace Whisperer;
using Timer = System.Timers.Timer;

public class CachedIndex<T> where T : IEquatable<T>
{
    private const string InitFileName = "init.txt";
    private string _baseDir;
    
    private bool _subdirectory = true;
    private string CurrentDirectory => Path.Combine(_baseDir, _subdirectory.ToString());
    private string NextDirectory => Path.Combine(_baseDir, (!_subdirectory).ToString());
    private IndexingOptions<T> _indexingOptions;
    
    private readonly Timer _timer;

    private Index<T> _index;

    private Func<IEnumerable<T>> _populateFunc;
    
    
    private EventHandler? _onCacheInitStarted;
    /// <summary>
    /// Is triggered when cache load is initialized.
    /// </summary>
    public event EventHandler CacheInitStarted
    {
        add => _onCacheInitStarted += value;
        remove => _onCacheInitStarted -= value;
    }
    
    /// <summary>
    /// Is trigered when cache load finishes. Retruns true if finishes successfully, otherwise returns false.
    /// </summary>
    private EventHandler<bool>? _onCacheInitFinished;
    public event EventHandler<bool> CacheInitFinished
    {
        add => _onCacheInitFinished += value;
        remove => _onCacheInitFinished -= value;
    }

    public CachedIndex(string baseDir,
        TimeSpan expiration,
        Func<IEnumerable<T>> populateFunc,
        IndexingOptions<T> indexingOptions)
    {
        _baseDir = baseDir;
        _populateFunc = populateFunc;
        _indexingOptions = indexingOptions;
        
        InitLatestDirectory();

        _index = new Index<T>(CurrentDirectory, _indexingOptions.IndexAnalyzer, _indexingOptions.QueryAnalyzer);


        // set timer for cache renewal
        _timer = new Timer(expiration.TotalMilliseconds)
        {
            Enabled = true,
            AutoReset = true,
        };
        _timer.Elapsed += (_, _) => _ = RefreshCacheAsync();
        _timer.Start();
        
        // try to renewCache
        _ = Task.Run(async () => await RefreshCacheAsync() );
    }

    private void InitLatestDirectory()
    {
        var path = Path.Combine(_baseDir, InitFileName);
        if (File.Exists(path))
        {
            string value = File.ReadAllText(path);
            bool.TryParse(value, out _subdirectory);
        }
    }

    private async Task SaveLatestDirectoryAsync()
    {
        var path = Path.Combine(_baseDir, InitFileName);
        await File.WriteAllTextAsync(path, _subdirectory.ToString());
    }

    /// <summary>
    /// Forces to refresh cache - use with care
    /// </summary>
    public async Task RefreshCacheAsync()
    {
        try
        {
            if(_onCacheInitStarted != null)
                _onCacheInitStarted.Invoke(this, EventArgs.Empty);
            
            var nextIndex = new Index<T>(NextDirectory, _indexingOptions.IndexAnalyzer, _indexingOptions.QueryAnalyzer);
            // prepare other directory
            var newIndex = FillIndex(nextIndex);
            _index = newIndex;

            // we do not cleanup current directory now, so we do not have to solve concurrency issues
            // (can't delete index while any search is in progress)
            // so we leave cleanup to next folder switch (RenewCache call)

            _subdirectory = !_subdirectory; //switching directories
            await SaveLatestDirectoryAsync();
            
            await Task.Delay(TimeSpan.FromMinutes(3)); // leave some time for running queries to finish  
            CleanPreviousDirectory(NextDirectory);
            
            if(_onCacheInitFinished != null)
                _onCacheInitFinished.Invoke(this, true);
        }
        catch (Exception e)
        {
            if(_onCacheInitFinished != null)
                _onCacheInitFinished.Invoke(this, false);
            throw;
        }
    }

    public IEnumerable<T> Search(string query, int numResults = 10, string? filter = null)
    {
        return _index.Search(query, numResults, filter);
    }

    private void CleanPreviousDirectory(string path)
    {
        if (Directory.Exists(path))
            Directory.Delete(path, true);
        Directory.CreateDirectory(path);
    }

    private Index<T> FillIndex(Index<T> index)
    {
        var data = _populateFunc.Invoke();
        index.AddDocuments(data,
            _indexingOptions.TextSelector,
            _indexingOptions.BoostSelector,
            _indexingOptions.FilterSelector);
        return index;
    }

    public void Dispose()
    {
        _timer.Stop();
        _timer.Dispose();
        _index?.Dispose();
    }
}