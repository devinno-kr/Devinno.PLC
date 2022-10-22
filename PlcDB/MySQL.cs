using Devinno.PLC.Library;
using System;
using System.Collections.Generic;

namespace PlcDB
{
    public class MySQL : ILadderLibrary
    {
        #region Properties
        public string Name => "MySQL";
        #endregion

        #region Member Variable
        private Devinno.Database.MySQL DB = new Devinno.Database.MySQL();
        #endregion

        #region Constructor
        public MySQL()
        {

        }
        #endregion

        #region Method
        #region Begin
        public void Begin(string Host, string Database, string ID, string Password, int Port = 3306, string ConnectStringOptions = null)
        {
            
            DB.Host = Host;
            DB.Port = Port;
            DB.DatabaseName = Database;
            DB.ID = ID;
            DB.Password = Password;
            DB.ConnectStringOptions = ConnectStringOptions;
        }
        #endregion

        #region Query
        public void Query(string sql)
        {
            try
            {
                DB.Execute((conn, cmd, trans) =>
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                });
            }
            catch { }
        }
        #endregion

        #region Insert
        public void Insert(string Table, string Data)
        {
            #region Values
            string names = "";
            string values = "";
            foreach (var v in Data.Split(','))
            {
                var s = v.Split('=');
                if (s.Length == 2)
                {
                    names += s[0] + ",";
                    values += s[1] + ",";
                }
            }

            var sql = $"INSERT INTO {Table}({names.Substring(0, names.Length - 1)}) Value ({values.Substring(0, values.Length - 1)})";
            #endregion
            
            Query(sql);
        }
        #endregion
        #endregion

    }
}
