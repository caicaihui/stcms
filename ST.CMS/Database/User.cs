using System;
using System.Data;
using System.Collections.Generic;
using ST.Framework.Database;

namespace ST.CMS.Database
{
    public class DUser
    {
        public static readonly string table = "st_admin";

        public static DataView List()
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("table", table);
            d.Add("order", "id");

            return ST.Framework.Database.DBShortcut.Select(d).Tables[0].DefaultView;
        }

        public static DataView Details(string name)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("table", table);
            d.Add("where", "username=@0");
            d.Add("list", new List<string>() { name });

            return ST.Framework.Database.DBShortcut.Select(d).Tables[0].DefaultView;
        }

        public static DataView Details(string name, string password)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("table", table);
            d.Add("where", "username=@0 and password=@1");
            d.Add("list", new List<string>() { name, password });

            return ST.Framework.Database.DBShortcut.Select(d).Tables[0].DefaultView;
        }

        public static void UpdateStyle(string layout, string color, string name)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("userstyle", String.Format("{0} {1}", layout, color));

            ST.Framework.Database.DBShortcut.Update(table, d, "username=@0", new List<string>() { name });
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
