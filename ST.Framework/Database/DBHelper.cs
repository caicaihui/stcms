using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Dynamic;

namespace ST.Framework.Database
{
    public class DBHelper
    {
        private DbConnection MyConnection;
        private DbCommand MyCommand;
        private DbProviderFactory MyFactory;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string ProviderName
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        #region DBHelper
        public DBHelper()
            : this("DataLink")
        {
        }

        public DBHelper(string name)
            : this(ConnectionName(name).ProviderName, ConnectionName(name).ConnectionString)
        {
        }

        public DBHelper(string providerName, string connectionString)
        {
            this.ProviderName = providerName;
            this.ConnectionString = ConnectionPath(connectionString);

            MyFactory = DbProviderFactories.GetFactory(ProviderName);
            MyConnection = MyFactory.CreateConnection();
            MyConnection.ConnectionString = ConnectionString;
            MyConnection.Open();

            MyCommand = MyConnection.CreateCommand();
        }
        #endregion

        #region Connection
        /// <summary>
        /// 数据连接实体
        /// </summary>
        public static ConnectionStringSettings ConnectionName(string name)
        {
            return ConfigurationManager.ConnectionStrings[name];
        }

        /// <summary>
        /// 处理数据库连接路径
        /// </summary>
        public string ConnectionPath(string connectionString)
        {
            return String.Format(connectionString, System.Web.HttpContext.Current.Server.MapPath("~/"));
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                MyCommand.CommandText = sql;
                return MyCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.Dispose();
                throw e;
            }
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        public int ExecuteNonQuery(List<String> list)
        {
            try
            {
                int count = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    MyCommand.CommandText = list[i];
                    count += MyCommand.ExecuteNonQuery();
                }
                return count;
            }
            catch (Exception e)
            {
                this.Dispose();
                throw e;
            }
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        public object ExecuteScalar(string sql)
        {
            try
            {
                MyCommand.CommandText = sql;
                return MyCommand.ExecuteScalar();
            }
            catch (Exception e)
            {
                this.Dispose();
                throw e;
            }
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        public DataSet ExecuteDataSet(string sql)
        {
            try
            {
                MyCommand.CommandText = sql;
                DbDataAdapter dataAdapter = MyFactory.CreateDataAdapter();
                dataAdapter.SelectCommand = MyCommand;

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                return dataSet;
            }
            catch (Exception e)
            {
                this.Dispose();
                throw e;
            }
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// ExecuteReader
        /// </summary>
        public List<dynamic> ExecuteReader(string sql)
        {
            try
            {
                var list = new List<dynamic>();

                MyCommand.CommandText = sql;
                using (DbDataReader dr = MyCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        IDictionary<string, object> d = new System.Dynamic.ExpandoObject();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            d.Add(dr.GetName(i), dr.GetValue(i));
                        }
                       list.Add(d);
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                this.Dispose();
                throw e;
            }
        }
        #endregion

        #region Utils
        /// <summary>
        /// 参数组
        /// </summary>
        public void ParameterAdd(string parameterName, object value)
        {
            DbParameter MyDbParameter = MyCommand.CreateParameter();
            MyDbParameter.ParameterName = parameterName;
            MyDbParameter.Value = value;
            MyDbParameter.DbType = DbType.String;
            MyCommand.Parameters.Add(MyDbParameter);
        }

        /// <summary>
        /// 释放所有资源
        /// </summary>
        public void Close()
        {
            this.Dispose();
        }

        /// <summary>
        /// 释放所有资源
        /// </summary>
        public void Dispose()
        {
            if (MyCommand.Parameters != null)
                MyCommand.Parameters.Clear();

            if (MyCommand != null)
                MyCommand.Dispose();

            //if (MyConnection.State == ConnectionState.Open)
            if (MyConnection != null)
            {
                MyConnection.Close();
                MyConnection = null;
            }

        }
        #endregion
    }
}
