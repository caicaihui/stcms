using System;
using System.Data;
using System.Collections.Generic;
using ST.Framework.Database;
using ST.Framework.Utils;

namespace ST.CMS.Database
{
    public class DContent
    {
        public static readonly string table = "st_content";

        public static DataView List(string id)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("table", table);
            d.Add("where", "classid=@0");
            d.Add("list", new List<string>() { id });
            d.Add("order", "inputdate desc, id");

            return ST.Framework.Database.DBShortcut.Select(d).Tables[0].DefaultView;
        }

        public static DataView Details(string id)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("table", table);
            d.Add("where", "id=@0");
            d.Add("list", new List<string>() { id });

            return ST.Framework.Database.DBShortcut.Select(d).Tables[0].DefaultView;
        }

        public static DataView Form(string action, string id)
        {
            if (action == "update")
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("table", table);
                d.Add("where", "id=@0");
                d.Add("list", new List<string>() { id });
                return DBShortcut.Select(d).Tables[0].DefaultView;
            }
            else
            {
                DataView dv = DBShortcut.Select(new Dictionary<string, object>() { { "table", table }, { "top", "1" } }).Clone().Tables[0].DefaultView;
                DataRow dr = dv.Table.Rows.Add();
                dr["id"] = "0";
                dr["inputdate"] = DateTime.Now;
                return dv;
            }
        }

        public static void Update(Dictionary<string, object> d, string id)
        {
            ST.Framework.Database.DBShortcut.Update(table, d, "id=@0", new List<string>() { id });
        }

        public static void Insert(Dictionary<string, object> d)
        {
            ST.Framework.Database.DBShortcut.Insert(table, d);
        }

        public static void Delete(string id)
        {
            ST.Framework.Database.DBShortcut.Delete(table, "id=@0", new List<string>() { id });
        }
    }
}
