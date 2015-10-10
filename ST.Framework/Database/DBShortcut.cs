using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace ST.Framework.Database
{
    public class DBShortcut
    {

        #region 提取数据
        /// <summary>
        /// 筛选
        /// </summary>
        public static DataSet Select(Dictionary<string, object> d)
        {
            string table = d["table"].ToString();
            int top = d.ContainsKey("top") ? Convert.ToInt32(d["top"]) : 0;
            string field = d.ContainsKey("field") ? d["field"].ToString() : String.Empty;
            string where = d.ContainsKey("where") ? d["where"].ToString() : String.Empty;
            List<string> list = d.ContainsKey("list") ? (List<string>)d["list"] : null;
            string order = d.ContainsKey("order") ? d["order"].ToString() : String.Empty;

            return Select(table, top, field, where, order, list);
        }

        /// <summary>
        /// 筛选
        /// </summary>
        public static DataSet Select(Dictionary<string, object> d, out Dictionary<string, object> page)
        {
            string table = d["table"].ToString();
            int top = d.ContainsKey("top") ? Convert.ToInt32(d["top"]) : 0;
            string field = d.ContainsKey("field") ? d["field"].ToString() : String.Empty;
            string where = d.ContainsKey("where") ? d["where"].ToString() : String.Empty;
            List<string> list = d.ContainsKey("list") ? (List<string>)d["list"] : null;
            string order = d.ContainsKey("order") ? d["order"].ToString() : String.Empty;

            page = new Dictionary<string, object>();
            page.Clear();

            if (d.ContainsKey("pagesize"))
            {

                int count = Count(table);
                int pagesize = Convert.ToInt32(d["pagesize"]);
                int pagecount = (count + pagesize - 1) / pagesize;
                var dpageindex = d["pageindex"];
                int pageindex = dpageindex == null ? 1 : Convert.ToInt32(dpageindex);
                string pageformat = d.ContainsKey("pageformat") ? d["pageformat"].ToString() : "<a href=\"?page={0}\">{1}</a>";
                string pagefirstformat = d.ContainsKey("pagefirstformat") ? d["pagefirstformat"].ToString() : pageformat;
                string pageprevformat = d.ContainsKey("pageprevformat") ? d["pageprevformat"].ToString() : pageformat;
                string pagenextformat = d.ContainsKey("pagenextformat") ? d["pagenextformat"].ToString() : pageformat;
                string pagelastformat = d.ContainsKey("pagelastformat") ? d["pagelastformat"].ToString() : pageformat;

                StringBuilder pagenum = new StringBuilder();
                int j = (pageindex - 5 < 1) ? (1) : (pageindex - 5);
                int k = (pageindex + 5 > pagecount) ? (pagecount) : (pageindex + 5);

                for (int i = j; i < k + 1; i++)
                {
                    if (pageindex == i)
                        pagenum.AppendFormat("<span>{0}</span>", i);
                    else
                        pagenum.AppendFormat(pageformat, i, i);
                }

                string pagefirst = pageindex == 1 ? String.Empty : String.Format(pagefirstformat, "1", "首页");
                string pageprev = pageindex == 1 ? String.Empty : String.Format(pageprevformat, pageindex - 1, "上一页");
                string pagenext = (pageindex == pagecount || count == 0) ? String.Empty : String.Format(pagenextformat, pageindex + 1, "下一页");
                string pagelast = (pageindex == pagecount || count == 0) ? String.Empty : String.Format(pagelastformat, pagecount, "尾页");

                page.Add("count", count);
                page.Add("pagesize", pagesize);
                page.Add("pagecount", pagecount);
                page.Add("pageindex", pageindex);
                page.Add("pagenum", pagenum);
                page.Add("pagefirst", pagefirst);
                page.Add("pageprev", pageprev);
                page.Add("pagenext", pagenext);
                page.Add("pagelast", pagelast);

                return Sql(DBSql.Page(table, pagesize, pageindex, "id", where, order));
            }

            return Select(table, top, field, where, order, list);
        }

        /// <summary>
        /// 筛选
        /// </summary>
        public static DataSet Select(string table)
        {
            if (table.Contains(" "))
                return Sql(table);
            else
                return Select(table, 0, String.Empty, String.Empty, String.Empty, null);
        }

        /// <summary>
        /// 筛选
        /// </summary>
        public static DataSet Select(string table, int top, string field, string where, string order, List<string> list = null)
        {
            DBHelper db = new DBHelper();

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    db.ParameterAdd("@" + i, list[i]);
                }
            }

            DataSet ds = db.ExecuteDataSet(DBSql.Select(table, top, field, where, order));
            db.Dispose();

            return ds;
        }
        #endregion

        #region 提取数据 动态类型
        /// <summary>
        /// 筛选
        /// </summary>
        public static dynamic Query(Dictionary<string, object> d)
        {
            string table = d["table"].ToString();
            int top = d.ContainsKey("top") ? Convert.ToInt32(d["top"]) : 0;
            string field = d.ContainsKey("field") ? d["field"].ToString() : String.Empty;
            string where = d.ContainsKey("where") ? d["where"].ToString() : String.Empty;
            List<string> list = d.ContainsKey("list") ? (List<string>)d["list"] : null;
            string order = d.ContainsKey("order") ? d["order"].ToString() : String.Empty;

            return Query(table, top, field, where, order, list);
        }
        
        /// <summary>
        /// 筛选
        /// </summary>
        public static dynamic Query(Dictionary<string, object> d, out dynamic dy)
        {
            string table = d["table"].ToString();
            int top = d.ContainsKey("top") ? Convert.ToInt32(d["top"]) : 0;
            string field = d.ContainsKey("field") ? d["field"].ToString() : String.Empty;
            string where = d.ContainsKey("where") ? d["where"].ToString() : String.Empty;
            List<string> list = d.ContainsKey("list") ? (List<string>)d["list"] : null;
            string order = d.ContainsKey("order") ? d["order"].ToString() : String.Empty;

            dy = new System.Dynamic.ExpandoObject();

            if (d.ContainsKey("pagesize"))
            {

                int count = Count(table);
                int pagesize = Convert.ToInt32(d["pagesize"]);
                int pagecount = (count + pagesize - 1) / pagesize;
                var dpageindex = d["pageindex"];
                int pageindex = dpageindex == null ? 1 : Convert.ToInt32(dpageindex);
                string pageformat = d.ContainsKey("pageformat") ? d["pageformat"].ToString() : "<a href=\"?page={0}\">{1}</a>";
                string pagefirstformat = d.ContainsKey("pagefirstformat") ? d["pagefirstformat"].ToString() : pageformat;
                string pageprevformat = d.ContainsKey("pageprevformat") ? d["pageprevformat"].ToString() : pageformat;
                string pagenextformat = d.ContainsKey("pagenextformat") ? d["pagenextformat"].ToString() : pageformat;
                string pagelastformat = d.ContainsKey("pagelastformat") ? d["pagelastformat"].ToString() : pageformat;

                StringBuilder pagenum = new StringBuilder();
                int j = (pageindex - 5 < 1) ? (1) : (pageindex - 5);
                int k = (pageindex + 5 > pagecount) ? (pagecount) : (pageindex + 5);

                for (int i = j; i < k + 1; i++)
                {
                    if (pageindex == i)
                        pagenum.AppendFormat("<span>{0}</span>", i);
                    else
                        pagenum.AppendFormat(pageformat, i, i);
                }

                string pagefirst = pageindex == 1 ? String.Empty : String.Format(pagefirstformat, "1", "首页");
                string pageprev = pageindex == 1 ? String.Empty : String.Format(pageprevformat, pageindex - 1, "上一页");
                string pagenext = (pageindex == pagecount || count == 0) ? String.Empty : String.Format(pagenextformat, pageindex + 1, "下一页");
                string pagelast = (pageindex == pagecount || count == 0) ? String.Empty : String.Format(pagelastformat, pagecount, "尾页");

                dy.count = count;
                dy.pagesize = pagesize;
                dy.pagecount = pagecount;
                dy.pageindex = pageindex;
                dy.pagenum = pagenum;
                dy.pagefirst = pagefirst;
                dy.pageprev = pageprev;
                dy.pagenext = pagenext;
                dy.pagelast = pagelast;

                return SqlReader(DBSql.Page(table, pagesize, pageindex, "id", where, order));
            }

            return Query(table, top, field, where, order, list);
        }

        /// <summary>
        /// 筛选
        /// </summary>
        public static dynamic Query(string table)
        {
            return Query(table, 0, String.Empty, String.Empty, String.Empty, null);
        }

        /// <summary>
        /// 筛选
        /// </summary>
        public static dynamic Query(string table, int top, string field, string where, string order, List<string> list = null)
        {
            DBHelper db = new DBHelper();
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    db.ParameterAdd("@" + i, list[i]);
                }
            }

            dynamic d = db.ExecuteReader(DBSql.Select(table, top, field, where, order));
            db.Dispose();

            return d;
        }
        #endregion

        #region 提取数据 sql语句
        /// <summary>
        /// 筛选
        /// </summary>
        public static dynamic SqlReader(string sql, List<string> list = null)
        {
            DBHelper db = new DBHelper();

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    db.ParameterAdd("@" + i, list[i]);
                }
            }

            dynamic dy = db.ExecuteReader(sql);
            db.Dispose();

            return dy;
        }

        /// <summary>
        /// 筛选
        /// </summary>
        public static DataSet Sql(string sql, List<string> list = null)
        {
            DBHelper db = new DBHelper();

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    db.ParameterAdd("@" + i, list[i]);
                }
            }

            DataSet ds = db.ExecuteDataSet(sql);
            db.Dispose();

            return ds;
        }
        #endregion

        #region 添加数据
        /// <summary>
        /// 添加
        /// </summary>
        public static int Insert(string table, Dictionary<string, object> field)
        {
            DBHelper db = new DBHelper();
            StringBuilder sql = new StringBuilder();

            string key = String.Empty;
            string value = String.Empty;
            foreach (KeyValuePair<string, object> s in field)
            {
                key += String.Format("[{0}],", s.Key);
                value += String.Format("@{0},", s.Key);
                db.ParameterAdd("@" + s.Key, s.Value);
            }
            sql.AppendFormat("insert into [{0}](", table);
            sql.Append(key.TrimEnd(','));
            sql.Append(")values(");
            sql.Append(value.TrimEnd(','));
            sql.Append(")");

            int row = db.ExecuteNonQuery(sql.ToString());
            db.Dispose();

            return row;
        }
        #endregion

        #region 更新数据
        /// <summary>
        /// 更新
        /// </summary>
        public static int Update(string table, Dictionary<string, object> field, string where, List<string> list = null)
        {
            DBHelper db = new DBHelper();
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("update [{0}] set ", table);
            foreach (KeyValuePair<string, object> s in field)
            {
                sql.AppendFormat("[{0}]=@{0},", s.Key);
                db.ParameterAdd("@" + s.Key, s.Value);
            }
            sql.Remove(sql.Length - 1, 1);
            if (where != "")
            {
                sql.AppendFormat(" where {0}", where);
            }

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    db.ParameterAdd("@" + i, list[i]);
                }
            }

            int row = db.ExecuteNonQuery(sql.ToString());
            db.Dispose();

            return row;
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除
        /// </summary>
        public static int Delete(string table, string where, List<string> list = null)
        {
            DBHelper db = new DBHelper();

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    db.ParameterAdd("@" + i, list[i]);
                }
            }

            int row = db.ExecuteNonQuery(DBSql.Delete(table, where));
            db.Dispose();

            return row;
        }
        #endregion

        #region 统计数据
        /// <summary>
        /// 统计
        /// </summary>
        public static int Count(string table)
        {
            return Count(table, String.Empty, null);
        }

        /// <summary>
        /// 统计
        /// </summary>
        public static int Count(string table, string where, List<string> list = null)
        {
            DBHelper db = new DBHelper();

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    db.ParameterAdd("@" + i, list[i]);
                }
            }

            int row = Convert.ToInt32(db.ExecuteScalar(DBSql.Count(table, where)).ToString());
            db.Dispose();

            return row;
        }
        #endregion

        #region 是否存在数据
        /// <summary>
        /// 是否存在
        /// </summary>
        public static bool Exists(string table, string where, List<string> list = null)
        {
            if (Count(table, where, list) > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 返回单行数据
        /// <summary>
        /// 单行数据
        /// </summary>
        public static string Row(string table, string field, string where, List<string> list = null)
        {
            DBHelper db = new DBHelper();

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    db.ParameterAdd("@" + i, list[i]);
                }
            }

            string row = db.ExecuteScalar(DBSql.Select(table, 1, field, where, String.Empty)).ToString();
            db.Dispose();

            return row;
        }
        #endregion
    }
}
