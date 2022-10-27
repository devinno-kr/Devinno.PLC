using Devinno.PLC.Library;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Loader;
using Thread = System.Threading.Thread;

namespace LadderDB
{
    public class MySQL : ILadderLibrary
    {
        #region Properties
        public string LibraryName => "MySQL";

        private string Host { get; set; } = "localhost";
        private int Port { get; set; } = 3306;
        private string ID { get; set; } = "root";
        private string Password { get; set; } = "1234";
        private string DatabaseName { get; set; }
        private string ConnectStringOptions { get; set; } = "";
        #endregion

        #region Member Variable
        private Queue<string> Q = new Queue<string>();
        private Thread th;
        private bool bStart = false;
        #endregion

        #region Constructor
        public MySQL()
        {
           
        }
        #endregion

        #region Method
        #region Begin / End
        public void Begin()
        {
            #region Thread
            th = new Thread(() =>
            {
                bStart = true;
                while (bStart)
                {
                    if (Q.Count > 0)
                    {
                        try
                        {
                            string ConnectString = $"Server={Host};Port={Port};Database={DatabaseName};Uid={ID};pwd={Password};" + ConnectStringOptions;
                            using (var conn = new MySqlConnection(ConnectString))
                            {
                                conn.Open();
                                using (var cmd = conn.CreateCommand())
                                {
                                    using (var trans = conn.BeginTransaction())
                                    {
                                        try
                                        {
                                            cmd.CommandText = Q.Dequeue();
                                            cmd.ExecuteNonQuery();
                                            trans.Commit();
                                        }
                                        catch (Exception ex)
                                        {
                                            try { trans.Rollback(); }
                                            catch (SqlException ex2) { }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { }
                    }
                    Thread.Sleep(10);
                }
            })
            { IsBackground = true };
            th.Start();
            #endregion
        }

        public void End()
        {
            bStart = false;
        }
        #endregion

        #region Begin
        /// <summary>
        /// . 문법
        /// [인스턴스].Begin(Host, Database, ID, Password, Port, ConnectStringOptions)
        /// 
        /// Host : 서버주소
        /// Database : 데이터베이스 명
        /// ID : 아이디
        /// Password : 비밀번호
        /// Port : 포트번호, 기본값:3306
        /// ConnectStringOptions : 연결 문자열 옵션
        /// 
        /// . 설명
        /// 데이터베이스 연결 정보 설정
        /// </summary>
        public void Begin(string Host, string Database, string ID, string Password, int Port = 3306, string ConnectStringOptions = null)
        {
            this.Host = Host;
            this.Port = Port;
            this.DatabaseName = Database;
            this.ID = ID;
            this.Password = Password;
            this.ConnectStringOptions = ConnectStringOptions;
        }
        #endregion

        #region Query
        /// <summary>
        /// . 문법
        /// [인스턴스].Query(SQL)
        /// 
        /// SQL : SQL 구문
        /// 
        /// . 설명
        /// SQL 쿼리 실행
        /// </summary>
        public void Query(string sql)
        {
            Q.Enqueue(sql);
        }
        #endregion

        #region Insert
        /// <summary>
        /// . 문법
        /// [인스턴스].Insert(Table, Data)
        /// 
        /// Table : 테이블 명
        /// Data : 데이터, 예) Temp=36.5,Hum=33,EvTime='2022-11-01 12:30:00'";
        /// 
        /// . 설명
        /// 레코드 추가
        /// </summary>
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
