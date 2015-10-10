using System;
using System.Data;
using System.Collections.Generic;
using ST.Framework.Database;
using ST.Framework.Utils;

namespace ST.CMS.Database
{
    public class DModel
    {
        public static readonly string table = "st_model";

        public static DataSet ModelCache
        {
            get
            {
                if (UCache.Get(table) == null)
                {
                    DataSet ds = DBShortcut.Select(table, 0, String.Empty, String.Empty, "id");
                    UCache.Set(table, ds);
                    return ds;
                }
                return (System.Data.DataSet)UCache.Get(table);
            }
        }

        public static DataView List()
        {
            return ModelCache.Tables[0].DefaultView;
        }

        public static DataView Details(string id)
        {
            return new DataView(ModelCache.Tables[0], "id=" + id, "", DataViewRowState.CurrentRows);
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
