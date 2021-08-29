﻿using DatabaseUpgrader;


namespace HlidacStatu.DBUpgrades
{
    public static partial class DBUpgrader
    {

        private partial class UpgradeDB
        {

            [DatabaseUpgradeMethod("1.0.0.52")]
            public static void Init_1_0_0_52(IDatabaseUpgrader du)
            {

                du.AddColumnToTable("IsInRS", "smallint", "Firma", true);

                //du.RunDDLCommands(sql);


            }




        }

    }
}
