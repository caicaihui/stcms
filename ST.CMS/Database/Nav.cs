using System;
using System.Data;
using System.Collections.Generic;
using ST.Framework.Database;
using ST.Framework.Utils;

namespace ST.CMS.Database
{
    public class DNav
    {
        public static readonly string table = "st_nav";

        public static DataSet NavCache
        {
            get
            {
                if (UCache.Get(table) == null)
                {
                    DataSet ds = DBShortcut.Select(table, 0, String.Empty, String.Empty, "ordernum, id desc");
                    UCache.Set(table, ds);
                    return ds;
                }
                return (System.Data.DataSet)UCache.Get(table);
            }
        }

        public static DataView List(string id)
        {
            //Dictionary<string, object> d = new Dictionary<string, object>();
            //d.Add("table", table);
            //d.Add("where", "parentid=@0");
            //d.Add("list", new List<string>() { parentid });

            //return ST.Framework.Database.DBShortcut.Select(d).Tables[0].Rows;

            DataView dv = NavCache.Tables[0].DefaultView;
            dv.RowFilter = "parentid=" + id;
            return dv;
        }

        public static DataView Details(string id)
        {
            return new DataView(NavCache.Tables[0], "id=" + id, "", DataViewRowState.CurrentRows);
        }

        public static string[] Parent(object action, object id, string text)
        {
            if (action == null || id == null)
                return new string[] { "0", text };

            if (action.ToString() == "update")
            {
                string parentid = new DataView(NavCache.Tables[0], "id=" + id, "", DataViewRowState.CurrentRows)[0]["parentid"].ToString();

                if (parentid == "0")
                    return new string[] { "0", text };

                DataView dv = Details(parentid);
                return new string[] { parentid, dv[0]["name"].ToString() }; 
            }

            DataView dvinsert = Details(id.ToString());
            return new string[] { id.ToString(), dvinsert[0]["name"].ToString() }; 
            
        }

        public static DataView Form(string action, string id)
        {
            if (action == "update")
            {
                return Details(id);
            }
            else
            {
                DataView dv = DBShortcut.Select(new Dictionary<string, object>() { { "table", table }, { "top", "1" } }).Clone().Tables[0].DefaultView;
                DataRow dr = dv.Table.Rows.Add();
                dr["id"] = "0";
                dr["parentid"] = "0";
                dr["modelid"] = "0";
                dr["ordernum"] = "0";
                return dv;
            }
        }

        public static void Update(Dictionary<string, object> d, string id)
        {
            ST.Framework.Database.DBShortcut.Update(table, d, "id=@0", new List<string>() { id });
            UCache.Remove(table);
        }

        public static void Insert(Dictionary<string, object> d)
        {
            ST.Framework.Database.DBShortcut.Insert(table, d);
            UCache.Remove(table);
        }

        public static void Delete(string id)
        {
            ST.Framework.Database.DBShortcut.Delete(table, "id=@0", new List<string>() { id });
            UCache.Remove(table);
        }
    }
}
