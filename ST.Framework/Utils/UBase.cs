using System;
using System.Web;

namespace ST.Framework.Utils
{
    public class UBase
    {
        #region 获取页面地址的参数值，相当于 Request.QueryString public static string Get(string name)
        /// <summary>
        /// 获取页面地址的参数值，相当于 Request.QueryString
        /// </summary>
        /// <param name="name">name值</param>
        /// <returns>string</returns>
        public static string Get(string name)
        {
            string value = HttpContext.Current.Request.QueryString[name];
            return value == null ? string.Empty : value.Trim();
        }
        #endregion
    }
}
