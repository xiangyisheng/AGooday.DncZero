using AGooday.DncZero.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AGooday.DncZero.Common.DB
{
    public class BaseDBConfig
    {
        private static string defaultConnection = Appsettings.app(new string[] { "ConnectionStrings", "DefaultConnection" });
        private static string defaultConnectionFile = Appsettings.app(new string[] { "ConnectionStrings", "DefaultConnectionFile" });
        public static string ConnectionString => InitConn();

        public static string GetConnectionString(params string[] conn) => DifDBConnOfSecurity(conn);

        private static string InitConn() => DifDBConnOfSecurity(defaultConnectionFile, defaultConnection);
        private static string DifDBConnOfSecurity(params string[] conn)
        {
            try
            {
                foreach (var item in conn)
                {
                    try
                    {
                        if (File.Exists(item))
                        {
                            return File.ReadAllText(item).Trim();
                        }
                    }
                    catch (Exception) { }
                }

                return conn[conn.Length - 1];
            }
            catch (Exception)
            {
                throw new Exception("数据库连接字符串配置有误，请检查 web 层下  appsettings.json 文件");
            }
        }
    }
}
