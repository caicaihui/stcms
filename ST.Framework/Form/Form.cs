using System;
using System.Web;

namespace ST.Framework.Form
{
    public class Form
    {
        public static IHtmlString FormList(string field, string name, string defaultvalue, string bindvalue, string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (type == "textbox")
            {
                sb.AppendFormat(@"<input type=""text"" name=""st_model_{0}"" id=""st_model_{0}"" class=""form-control"" value=""{2}"" placeholder=""{1}"">", field, name, bindvalue);
            }

            if (type == "textarea")
            {
                sb.AppendFormat(@"<textarea id=""st_model_{0}"" name=""st_model_{0}"" class=""form-control"" rows=""5"" placeholder=""{1}"">{2}</textarea>", field, name, bindvalue);
            }

            if (type == "checkbox")
            {
                var item = defaultvalue.Split(',');

                for (int i = 0; i < item.Length; i++)
                {
                    var attr = item[i].Split('=');
                    string bind = ("," + bindvalue + ",").Contains("," + attr[1] + ",") ? " checked=\"checked\"" : String.Empty;

                    sb.AppendFormat(@"<label><input type=""checkbox"" name=""st_model_{0}"" value=""{2}""{3}>&nbsp;&nbsp;&nbsp;{1}</label>&nbsp;&nbsp;&nbsp;&nbsp;", field, attr[0], attr[1], bind);
                }
            }

            if (type == "radio")
            {
                var item = defaultvalue.Split(',');

                for (int i = 0; i < item.Length; i++)
                {
                    var attr = item[i].Split('=');
                    string bind = (bindvalue != attr[1] && i != 0) ? String.Empty : " checked=\"checked\"";

                    sb.AppendFormat(@"<label><input type=""radio"" name=""st_model_{0}"" value=""{2}""{3}>&nbsp;&nbsp;&nbsp;{1}</label>&nbsp;&nbsp;&nbsp;&nbsp;", field, attr[0], attr[1], bind);
                }
            }
            
            return new HtmlString(sb.ToString());
        }
    }
}
