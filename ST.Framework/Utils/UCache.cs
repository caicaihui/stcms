using System;
using System.Web;
using System.Web.Caching;

namespace ST.Framework.Utils
{
    /// <summary>
    /// 对Cache操作进行封装
    /// </summary>
    public class UCache
    {
        #region  获取缓存内容 public static object Get(string name)
        /// <summary>
        /// 获取缓存内容
        /// </summary>
        /// <param name="name">name值</param>
        /// <returns>object</returns>
        public static object Get(string name)
        {
            return HttpContext.Current.Cache.Get(name);
        }
        #endregion

        #region 设置缓存 public static void Set(string name, object value)
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="name">name值</param>
        /// <param name="value">value值</param>
        /// <returns>void</returns>
        public static void Set(string name, object value)
        {
            Set(name, value, null, DateTime.Now.AddMinutes(1000), TimeSpan.Zero);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="name">name值</param>
        /// <param name="value">value值</param>
        /// <param name="cacheDependency">所插入对象的文件依赖项或缓存键依赖项</param>
        /// <returns>void</returns>
        public static void Set(string name, object value, CacheDependency cacheDependency)
        {
            Set(name, value, cacheDependency, DateTime.Now.AddSeconds(20), TimeSpan.Zero);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="name">name值</param>
        /// <param name="value">value值</param>
        /// <param name="cacheDependency">所插入对象的文件依赖项或缓存键依赖项</param>
        /// <param name="dt">所插入对象将到期并被从缓存中移除的时间</param>
        /// <returns>void</returns>
        public static void Set(string name, object value, CacheDependency cacheDependency, DateTime dt)
        {
            Set(name, value, cacheDependency, dt, TimeSpan.Zero);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="name">name值</param>
        /// <param name="value">value值</param>
        /// <param name="cacheDependency">所插入对象的文件依赖项或缓存键依赖项</param>
        /// <param name="ts">最后一次访问所插入对象时与该对象到期时之间的时间间隔</param>
        /// <returns>void</returns>
        public static void Set(string name, object value, CacheDependency cacheDependency, TimeSpan ts)
        {
            Set(name, value, cacheDependency, Cache.NoAbsoluteExpiration, ts);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="name">name值</param>
        /// <param name="value">value值</param>
        /// <param name="cacheDependency">所插入对象的文件依赖项或缓存键依赖项</param>
        /// <param name="dt">所插入对象将到期并被从缓存中移除的时间</param>
        /// <param name="ts">最后一次访问所插入对象时与该对象到期时之间的时间间隔</param>
        /// <returns>void</returns>
        public static void Set(string name, object value, CacheDependency cacheDependency, DateTime dt, TimeSpan ts)
        {
            HttpContext.Current.Cache.Insert(name, value, cacheDependency, DateTime.Now.AddSeconds(10), Cache.NoSlidingExpiration);
        }
        #endregion

        #region 清除缓存 public static void Remove(string name)
        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="name">name值</param>
        /// <returns>void</returns>
        public static void Remove(string name)
        {
            object obj = HttpContext.Current.Cache[name];
            if (obj != null)
            {
                HttpContext.Current.Cache.Remove(name);
            }
        }
        #endregion

        #region 缓存数量
        /// <summary>
        /// 缓存数量
        /// </summary>
        /// <returns>string</returns>
        public static int Count
        {
            get
            {
                return HttpContext.Current.Cache.Count;
            }
        }
        #endregion
    }
}
