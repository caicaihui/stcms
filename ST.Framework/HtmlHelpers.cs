using System;
using System.Collections.Generic;
using System.Web;
using System.Web.WebPages.Html;

namespace ST.Framework
{
    public static class HtmlHelpers
    {

        public static IHtmlString Form(this HtmlHelper helper, string type, string name, string defaultvalue, string bindvalue)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (type == "textbox")
            {
                sb.AppendFormat(@"<input type=""text"" name=""{0}"" id=""{0}"" class=""form-control"" value=""{1}"" placeholder=""{1}"">", name, bindvalue);
            }

            return new HtmlString(sb.ToString());
        }

        public static IHtmlString SelectList(this HtmlHelper helper, string name, IDictionary<string, string> list, string selectvalue)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat("<select name=\"{0}\">", name);
            foreach (var k in list)
            {
                string defaultoption = selectvalue.Contains(k.Value) ? " selected=\"selected\"" : String.Empty;
                sb.AppendFormat("<option value=\"{0}\"{2}>{1}</option>", k.Value, k.Key, defaultoption);
            }
            sb.Append("</select>");
            return new HtmlString(sb.ToString());
        }

        public static IHtmlString RadioList(this HtmlHelper helper, string name, IDictionary<string, string> list, string selectvalue)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            int count = 0;

            foreach (var k in list)
            {
                count++;
                string defaultoption = (selectvalue != k.Value && count != 1) ? String.Empty : " checked=\"checked\"";

                sb.AppendFormat("<label><input type=\"radio\" name=\"{0}\" value=\"{1}\"{3}>&nbsp;&nbsp;&nbsp;{2}</label>&nbsp;&nbsp;&nbsp;&nbsp;", name, k.Value, k.Key, defaultoption);
            }
            return new HtmlString(sb.ToString());
        }

        public static IHtmlString CheckboxList(this HtmlHelper helper, string name, IDictionary<string, string> list, string selectvalue)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (var k in list)
            {
                string defaultoption = selectvalue.Contains(k.Value) ? " checked=\"checked\"" : String.Empty;
                sb.AppendFormat("<label><input type=\"checkbox\" name=\"{0}\" value=\"{1}\"{3}>&nbsp;&nbsp;&nbsp;{2}</label>&nbsp;&nbsp;&nbsp;&nbsp;", name, k.Value, k.Key, defaultoption);
            }
            return new HtmlString(sb.ToString());
        }
    }
}
