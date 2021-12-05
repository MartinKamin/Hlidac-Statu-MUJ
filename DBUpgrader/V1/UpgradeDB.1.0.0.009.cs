﻿using Devmasters.DatabaseUpgrader;


namespace HlidacStatu.DBUpgrades
{
    public static partial class DBUpgrader
    {

        private partial class UpgradeDB
        {

            [DatabaseUpgradeMethod("1.0.0.9")]
            public static void Init_1_0_0_9(IDatabaseUpgrader du)
            {
                string sql = @"

 update  WatchDog set PeriodId = 2

GO


";
                du.RunDDLCommands(sql);


            }




        }

    }
}
