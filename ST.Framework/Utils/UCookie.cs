using System;
using System.Web;

namespace ST.Framework.Utils
{
    /// <summary>
	/// 对Cookie操作进行封装
	/// </summary>
    public class UCookie
    {
        #region 获取Cookie值 public static HttpCookie Get(string name)
        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="name">name值</param>
        /// <returns>HttpCookie</returns>
        public static HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="name">name值</param>
        /// <returns>HttpCookie</returns>
        public static string GetValue(string name)
        {
            return UText.Decrypt(Get(name).Value);
        }
        #endregion

        #region 设置Cookie值 	public static HttpCookie Set(string name)
        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="name">name值</param>
        /// <returns>void</returns>
        public static void Set(string name)
        {
            Set(name, string.Empty, 0);
        }

        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="name">name值</param>
        /// <param name="value">value值</param>
        /// <returns>void</returns>
        public static void Set(string name, string value)
        {
            Set(name, value, 0);
        }

        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="name">name值</param>
        /// <param name="day">过期天数</param>
        /// <returns>void</returns>
        public static void Set(string name, int day)
        {
            Set(name, null, day);
        }

        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="name">name值</param>
        /// <param name="value">value值</param>
        /// <param name="day">过期天数</param>
        /// <returns>void</returns>
        public static void Set(string name, string value, int day)
        {
            System.Web.HttpCookie C = new HttpCookie(name);
            if (value != null)
            {
                C.Value = UText.Encrypt(value);
            }
            if (day > 0)
                C.Expires = System.DateTime.Now.AddDays(day);

            Save(C);
        }
        #endregion

        #region 保存Cookie值 public static void Save(HttpCookie cookie)
        /// <summary>
        /// 保存Cookie值
        /// </summary>
        /// <param name="cookie">cookie值</param>
        /// <returns>void</returns>
        public static void Save(HttpCookie cookie)
        {
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        #endregion

        #region 移除Cookie值 public static void Remove(HttpCookie cookie)
        /// <summary>
        /// 移除Cookie值
        /// </summary>
        /// <param name="cookie">cookie值</param>
        /// <returns>void</returns>
        public static void Remove(HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.Expires = new System.DateTime(1983, 5, 21);
                Save(cookie);
            }
        }
        #endregion

        #region 移除Cookie值 public static void Remove(string name)
        /// <summary>
        /// 移除Cookie值
        /// </summary>
        /// <param name="name">name值</param>
        /// <returns>void</returns>
        public static void Remove(string name)
        {
            Remove(Get(name));
        }
        #endregion
    }
}
