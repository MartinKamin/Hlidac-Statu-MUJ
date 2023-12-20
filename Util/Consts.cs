﻿using System;
using System.Text.RegularExpressions;
using Serilog;

namespace HlidacStatu.Util
{
    public static class Consts
    {

        public static char Ch = 'Ȼ';

        public static Devmasters.Batch.MultiOutputWriter outputWriter =
            new Devmasters.Batch.MultiOutputWriter(
                Devmasters.Batch.Manager.DefaultOutputWriter,
                new Devmasters.Batch.LoggerWriter(LW).OutputWriter
            );

        public static Devmasters.Batch.MultiProgressWriter progressWriter =
            new Devmasters.Batch.MultiProgressWriter(
                new Devmasters.Batch.ActionProgressWriter(0.1f).Writer,
                new Devmasters.Batch.ActionProgressWriter(10, new Devmasters.Batch.LoggerWriter(LW).ProgressWriter).Writer
            );

        public static RegexOptions DefaultRegexQueryOption = RegexOptions.IgnoreCase
                                                            | RegexOptions.IgnorePatternWhitespace
                                                            | RegexOptions.Multiline;

        public static System.Globalization.CultureInfo enCulture = System.Globalization.CultureInfo.InvariantCulture; //new System.Globalization.CultureInfo("en-US");
        public static System.Globalization.CultureInfo czCulture = System.Globalization.CultureInfo.GetCultureInfo("cs-CZ");
        public static System.Globalization.CultureInfo csCulture = System.Globalization.CultureInfo.GetCultureInfo("cs");
        public static Random Rnd = new Random();

        //todo: what to do here? IDK - we should get rid of devmasters log logger, since it can rewrite global logger
        public static Devmasters.Log.Logger LW = Devmasters.Log.Logger.CreateLogger("progress writer",
            new LoggerConfiguration().WriteTo.Console(), false);
    }
}
