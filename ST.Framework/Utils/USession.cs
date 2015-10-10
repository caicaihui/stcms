using System;
using System.Web;

namespace ST.Framework.Utils
{
    /// <summary>
    /// 对Session操作进行封装
    /// </summary>
    public class USession
    {
        #region 从 Session 读取 键为 name 的值 public static object Get(string name)
        /// <summary>
        /// 从 Session 读取 键为 name 的值
        /// </summary>
        /// <param name="name">name值</param>
        /// <returns>object</returns>
        public static object Get(string name)
        {
            return HttpContext.Current.Session[name];
        }
        #endregion

        #region 向 Session 保存 键为 name 的， 值为 value public static void Set(string name, object value)
        /// <summary>
        /// 向 Session 保存 键为 name 的， 值为 value
        /// </summary>
        /// <param name="name">name值</param>
        /// <param name="value">value值</param>
        /// <returns>void</returns>
        public static void Set(string name, object value)
        {
            HttpContext.Current.Session.Add(name, value);
        }
        #endregion

        #region 从 Session 删除 键为 name session 项 public static void Remove(string name)
        /// <summary>
        /// 从 Session 删除 键为 name session 项
        /// </summary>
        /// <param name="name">name值</param>
        /// <returns>void</returns>
        public static void Remove(string name)
        {
            if (HttpContext.Current.Session[name] != null)
            {
                HttpContext.Current.Session.Remove(name);
            }
        }
        #endregion

        #region 判断Session 是否存在
        /// <summary>
        /// 判断Session 是否存在
        /// </summary>
        /// <param name="name">name值</param>
        /// <returns>void</returns>
        public static bool Exists(string name)
        {
            return HttpContext.Current.Session[name] == null;
        }
        #endregion

        #region 删除所有 session 项
        /// <summary>
        /// 删除所有 session 项
        /// </summary>
        /// <returns>void</returns>
        public static void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }
        #endregion
    }
}
