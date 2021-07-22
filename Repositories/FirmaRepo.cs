using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Devmasters;
using HlidacStatu.Connectors.External;
using HlidacStatu.Entities;
using HlidacStatu.Lib.Data.External.DatoveSchranky;
using HlidacStatu.Extensions;

using static HlidacStatu.Entities.Firma;

namespace HlidacStatu.Repositories
{
    public static partial class FirmaRepo
    {
        static string cnnStr = Config.GetWebConfigValue("OldEFSqlConnection");


        public static void Save(Firma firma)
        {
            firma.JmenoAscii = TextUtil.RemoveDiacritics(firma.Jmeno);
            firma.SetTyp();

            string sql = @"exec Firma_Save @ICO,@DIC,@Datum_zapisu_OR,@Stav_subjektu,@Jmeno,@Jmenoascii,@Kod_PF,@Source, @Popis, @VersionUpdate, @krajId, @okresId, @status,@typ  ";
            string sqlNACE = @"INSERT into firma_NACE(ico, nace) values(@ico,@nace)";
            string sqlDS = @"INSERT into firma_DS(ico, DatovaSchranka) values(@ico,@DatovaSchranka)";

            string cnnStr = Config.GetWebConfigValue("OldEFSqlConnection");
            try
            {
                using (PersistLib p = new PersistLib())
                {
                    p.ExecuteNonQuery(cnnStr, CommandType.Text, sql, new IDataParameter[]
                    {
                        new SqlParameter("ico", firma.ICO),
                        new SqlParameter("dic", firma.DIC),
                        new SqlParameter("Datum_zapisu_OR", firma.Datum_Zapisu_OR),
                        new SqlParameter("Stav_subjektu", firma.Stav_subjektu),
                        new SqlParameter("Jmeno", firma.Jmeno),
                        new SqlParameter("Jmenoascii", firma.JmenoAscii),
                        new SqlParameter("Kod_PF", firma.Kod_PF),
                        new SqlParameter("Source", firma.Source),
                        new SqlParameter("Popis", firma.Popis),
                        new SqlParameter("VersionUpdate", firma.VersionUpdate),
                        new SqlParameter("KrajId", firma.KrajId),
                        new SqlParameter("OkresId", firma.OkresId),
                        new SqlParameter("Status", firma.Status),
                        new SqlParameter("Typ", firma.Typ),
                    });


                    if (firma.DatovaSchranka != null)
                    {
                        p.ExecuteNonQuery(cnnStr, CommandType.Text, "delete from firma_DS where ico=@ico",
                            new IDataParameter[]
                            {
                                new SqlParameter("ico", firma.ICO)
                            });
                        foreach (var ds in firma.DatovaSchranka.Distinct())
                        {
                            p.ExecuteNonQuery(cnnStr, CommandType.Text, sqlDS, new IDataParameter[]
                            {
                                new SqlParameter("ico", firma.ICO),
                                new SqlParameter("DatovaSchranka", ds),
                            });
                        }
                    }

                    if (firma.NACE != null)
                    {
                        p.ExecuteNonQuery(cnnStr, CommandType.Text, "delete from firma_NACE where ico=@ico",
                            new IDataParameter[]
                            {
                                new SqlParameter("ico", firma.ICO)
                            });
                        foreach (var nace in firma.NACE.Distinct())
                        {
                            p.ExecuteNonQuery(cnnStr, CommandType.Text, sqlNACE, new IDataParameter[]
                            {
                                new SqlParameter("ico", firma.ICO),
                                new SqlParameter("nace", nace),
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static Firma FromDS(string ds, bool getMissingFromExternal = false)
        {
            Firma f = FromDS(ds);
            if (!f.Valid && getMissingFromExternal)
            {
                var d = ISDS.GetSubjektyForDS(ds);
                if (d != null)
                {
                    return FromIco(d.ICO, getMissingFromExternal);
                }
            }

            return f;
        }

        public static Firma FromName(string jmeno, bool getMissingFromExternal = false)
        {
            Firma f = FromName(jmeno);
            if (f != null)
                return f;

            else if (getMissingFromExternal)
            {
                f = Merk.FromName(jmeno);
                if (f.Valid)
                    return f;

                if (f == null)
                    return Firma.NotFound;
                else if (f == Firma.NotFound || f == Firma.LoadError)
                    return f;

                f.RefreshDS();
                Save(f);
                return f;
            }
            else
            {
                return null;
            }
        }


        public static Firma FromIco(int ico, bool getMissingFromExternal = false)
        {
            return FromIco(ico.ToString().PadLeft(8, '0'), getMissingFromExternal);
        }

        public static Firma FromIco(string ico, bool getMissingFromExternal = false)
        {
            Firma f = FromIco(ico);

            if (f.Valid)
                return f;

            else if (getMissingFromExternal)
            {
                f = Merk.FromIco(ico);
                if (f.Valid)
                    return f;

                if (!f.Valid) //try firmo
                {
                    f = RZP.FromIco(ico);
                }

                if (f == null)
                    return Firma.NotFound;
                else if (f == Firma.NotFound || f == Firma.LoadError)
                    return f;

                f.RefreshDS();
                Save(f);
                return f;
            }
            else
            {
                return Firma.NotFound;
            }
        }
        
        public static void AddZahranicniFirma(string ico, string jmeno, string adresa)
        {
            using (PersistLib p = new PersistLib())
            {
                string sql = @"insert into firma(ico,dic,stav_subjektu, jmeno, jmenoascii, versionupdate, popis)
                                values(@ico,@dic,@stav,@jmeno,@jmenoascii,0,@adresa)";

                p.ExecuteNonQuery(cnnStr, CommandType.Text, sql, new IDataParameter[] {
                    new SqlParameter("ico", ico),
                    new SqlParameter("dic", ico),
                    new SqlParameter("stav", (int)1),
                    new SqlParameter("jmeno", jmeno),
                    new SqlParameter("jmenoascii", TextUtil.RemoveDiacritics(jmeno)),
                    new SqlParameter("versionupdate", (long)0),
                    new SqlParameter("adresa", TextUtil.ShortenText(adresa,100)),
                    });
            }
        }

        public static Firma FromIco(string ico)
        {
            Firma f = new Firma();
            using (PersistLib p = new PersistLib())
            {
                string sql = @"select * from Firma where ico = @ico";

                var res = p.ExecuteDataset(cnnStr, CommandType.Text, sql, new IDataParameter[] {
                        new SqlParameter("ico", ico)
                        });

                if (res.Tables[0].Rows.Count > 0)
                {
                    return FromDataRow(res.Tables[0].Rows[0]);
                }
                else
                {
                    return Firma.NotFound;
                }
            }
        }
        
        public static Firma FromName(string jmeno)
        {
            var res = AllFromName(jmeno);
            if (res.Count() == 0)
                return Firma.NotFound;
            else
                return res.First();
        }
        
        public static IEnumerable<Firma> AllFromName(string jmeno)
        {
            using (PersistLib p = new PersistLib())
            {
                string sql = @"select * from Firma where jmeno = @jmeno";

                var res = p.ExecuteDataset(cnnStr, CommandType.Text, sql, new IDataParameter[] {
                        new SqlParameter("jmeno", jmeno)
                        });

                if (res.Tables.Count > 0 && res.Tables[0].Rows.Count > 0)
                {
                    return res.Tables[0]
                        .AsEnumerable()
                        .Where(r => TextUtil.IsNumeric((string)r["ICO"]))
                        .Select(m => FromDataRow(m));
                }
                else
                    return new Firma[] { };
            }
        }

        public static IEnumerable<Firma> AllFromNameWildcards(string jmeno)
        {
            using (PersistLib p = new PersistLib())
            {
                var sql = @"select * from Firma where jmeno like @jmeno";

                var res = p.ExecuteDataset(cnnStr, CommandType.Text, sql, new IDataParameter[] {
                        new SqlParameter("jmeno", Firma.JmenoBezKoncovky(jmeno)+ "%")
                        });
                var found = new List<Firma>();
                if (res.Tables.Count > 0 && res.Tables[0].Rows.Count > 0)
                {
                    found.AddRange(res.Tables[0]
                        .AsEnumerable()
                        .Select(m => FromDataRow(m))
                        );
                    return found;
                }
                else
                    return new Firma[] { };
            }
        }

        public static Firma FromDS(string ds)
        {
            Firma f = new Firma();
            using (PersistLib p = new PersistLib())
            {
                string sql = @"select firma.* from Firma_ds fds inner join firma on firma.ico = fds.ico where DatovaSchranka = @ds";

                var res = p.ExecuteDataset(cnnStr, CommandType.Text, sql, new IDataParameter[] {
                        new SqlParameter("ds", ds)
                        });

                if (res.Tables[0].Rows.Count > 0)
                {
                    return FromDataRow(res.Tables[0].Rows[0]);
                }
                else
                {
                    return Firma.NotFound;
                }
            }

        }
        
        public static IEnumerable<string> AllIcoInRS()
        {
            using (PersistLib p = new PersistLib())
            {
                string sql = @"select ico from Firma where IsInRS = 1";

                var res = p.ExecuteDataset(cnnStr, CommandType.Text, sql, null);

                if (res.Tables.Count > 0 && res.Tables[0].Rows.Count > 0)
                {
                    var allIcos = res.Tables[0]
                        .AsEnumerable()
                        .Where(r => TextUtil.IsNumeric((string)r["ICO"]))
                        .Select(r => (string)r["ICO"])
                        .ToArray();
                    
                        return allIcos;
                }
                else
                    return new string[] { };
            }
        }
        
        public static IEnumerable<Firma> AllFirmyInRS(bool skipDS_Nace = false)
        {
            using (PersistLib p = new PersistLib())
            {
                string sql = @"select * from Firma where IsInRS = 1";

                var res = p.ExecuteDataset(cnnStr, CommandType.Text, sql, null);

                if (res.Tables.Count > 0 && res.Tables[0].Rows.Count > 0)
                {
                    return res.Tables[0]
                        .AsEnumerable()
                        .Where(r => TextUtil.IsNumeric((string)r["ICO"]))
                        .Select(r => FromDataRow(r, skipDS_Nace));
                }
                else
                    return new Firma[] { };
            }
        }

        private static Firma FromDataRow(DataRow dr, bool skipDS_Nace=false)
        {
            Firma f = new Firma();
            f.ICO = (string)dr["ico"];
            f.DIC = (string)PersistLib.IsNull(dr["dic"], string.Empty);
            f.Datum_Zapisu_OR = (DateTime?)PersistLib.IsNull(dr["datum_zapisu_or"], null);
            f.Stav_subjektu = Convert.ToInt32(PersistLib.IsNull(dr["Stav_subjektu"], 1));
            f.Status = Convert.ToInt32(PersistLib.IsNull(dr["Status"], 1));
            f.Jmeno = (string)PersistLib.IsNull(dr["jmeno"], string.Empty);
            f.JmenoAscii = (string)PersistLib.IsNull(dr["jmenoascii"], string.Empty);
            f.Kod_PF = (int?)PersistLib.IsNull(dr["Kod_PF"], null);
            f.VersionUpdate = (int)dr["VersionUpdate"];
            //f.VazbyRaw = (string)PersistLib.IsNull(dr["vazbyRaw"], (string)"[]");
            f.IsInRS = (short?)PersistLib.IsNull(dr["IsInRS"], null);
            f.KrajId = (string)PersistLib.IsNull(dr["krajid"], string.Empty);
            f.OkresId = (string)PersistLib.IsNull(dr["okresid"], string.Empty);
            f.Typ = (int?)PersistLib.IsNull(dr["typ"], null);

            if (skipDS_Nace == false)
            {
                using (PersistLib p = new PersistLib())
                {
                    f.DatovaSchranka = p.ExecuteDataset(cnnStr, CommandType.Text, "select DatovaSchranka from firma_DS where ico=@ico", new IDataParameter[] {
                        new SqlParameter("ico", f.ICO)
                        }).Tables[0]
                        .AsEnumerable()
                        .Select(m => m[0].ToString())
                        .ToArray();

                    f.NACE = p.ExecuteDataset(cnnStr, CommandType.Text, "select NACE from firma_Nace where ico=@ico", new IDataParameter[] {
                        new SqlParameter("ico", f.ICO)
                        }).Tables[0]
                        .AsEnumerable()
                        .Select(m => m[0].ToString())
                        .ToArray();
                }
            }
            return f;

        }

        public static string NameFromIco(string ico, bool IcoIfNotFound = false)
        {
            string cnnStr = Config.GetWebConfigValue("OldEFSqlConnection");
            using (PersistLib p = new PersistLib())
            {
                string sql = @"select jmeno from Firma where ico = @ico";

                var res = p.ExecuteScalar(cnnStr, CommandType.Text, sql, new IDataParameter[] {
                        new SqlParameter("ico", ico)
                        });

                if (PersistLib.IsNull(res) || string.IsNullOrEmpty(res as string))
                {
                    if (IcoIfNotFound)
                        return "IČO:" + ico;
                    else
                        return string.Empty;
                }
                else
                    return (string)res;

            }
        }
        
        public static void RefreshDS(this Firma firma)
        {
            firma.DatovaSchranka = Lib.Data.External.DatoveSchranky.ISDS.GetDatoveSchrankyForIco(firma.ICO);
        }

    }
}