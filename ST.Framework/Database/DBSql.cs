using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace ST.Framework.Database
{
    public class DBSql
    {

        /// <summary>
        /// 筛选
        /// </summary>
        public static string Select(string table, int top, string field, string where, string order)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ");
            if (top > 0)
            {
                sql.AppendFormat("top {0} ", top);
            }
            if (field != "")
            {
                sql.Append(field);
            }
            else
            {
                sql.Append("*");
            }
            sql.AppendFormat(" from [{0}] ", table);
            if (where != "")
            {
                sql.AppendFormat(" where {0} ", where);
            }
            if (order != "")
            {
                sql.AppendFormat(" order by {0} ", order);
            }
            return sql.ToString();
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static string Insert(string table, Dictionary<string, object> field)
        {
            StringBuilder sql = new StringBuilder();

            string key = String.Empty;
            string value = String.Empty;
            foreach (KeyValuePair<string, object> s in field)
            {
                key += String.Format("[{0}],", s.Key);
                value += String.Format("{0},", s.Value);
            }
            sql.AppendFormat("insert into [{0}](", table);
            sql.Append(key.TrimEnd(','));
            sql.Append(")values(");
            sql.Append(value.TrimEnd(','));
            sql.Append(")");
            return sql.ToString();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public static string Update(string table, Dictionary<string, object> field, string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update [{0}] set ", table);
            foreach (KeyValuePair<string, object> s in field)
            {
                sql.AppendFormat("[{0}]={1},", s.Key, s.Value);
            }
            sql.Remove(sql.Length - 1, 1);
            if (where != "")
            {
                sql.AppendFormat(" where {0}", where);
            }
            return sql.ToString();
        }

        /// <summary>
        /// 删除
        /// </summary>
        public static string Delete(string table, string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("delete from [{0}]", table);
            if (where != "")
            {
                sql.AppendFormat(" where {0}", where);
            }
            return sql.ToString();
        }

        /// <summary>
        /// 统计
        /// </summary>
        public static string Count(string table, string where)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select count(*) from [{0}]", table);
            if (where != "")
            {
                sql.AppendFormat(" where {0}", where);
            }
            return sql.ToString();
        }

        /// <summary>
        /// 分页
        /// </summary>
        public static string Page(string table, int pagesize, int pagecurrent, string fields, string where, string order)
        {
            StringBuilder sql = new StringBuilder();
            if (pagecurrent == 1)
            {
                sql.Append(Select(table, pagesize, String.Empty, where, order));
            }
            else
            {
                sql.AppendFormat("select top {0} * from {1} ", pagesize, table);
                sql.AppendFormat(" where {0} not in(select top {1} {0} from {2} ", fields, pagesize * (pagecurrent - 1), table);
                if (where != "")
                {
                    sql.AppendFormat(" where {0} ", where);
                }
                if (order != "")
                {
                    sql.AppendFormat(" order by {0} ", order);
                }
                sql.AppendFormat(")");
                if (where != "")
                {
                    sql.AppendFormat(" and {0} ", where);
                }
                if (order != "")
                {
                    sql.AppendFormat(" order by {0} ", order);
                }
            }

            return sql.ToString();

        }
    }
}
