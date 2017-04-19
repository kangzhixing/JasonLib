using System;

namespace JasonLib
{
    /// <summary>
    /// .net缓存策略
    /// </summary>
    public class JlHttpCache : ICache
    {
        private static object syncObject = new object();
        /// <summary>
        /// 构造方法
        /// </summary>
        protected JlHttpCache() { }

        private static JlHttpCache instance;
        /// <summary>
        /// 
        /// </summary>
        public static JlHttpCache Current
        {
            get
            {
                if (instance == null)
                {

                    lock (syncObject)
                    {
                        if (instance == null)
                        {
                            instance = new JlHttpCache();
                        }
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return System.Web.HttpContext.Current.Cache[key];
        }
        /// <summary>
        /// 获取缓存（泛型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return (T)System.Web.HttpContext.Current.Cache[key];
        }
        /// <summary>
        /// 加入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Add(string key, object obj)
        {
            System.Web.HttpContext.Current.Cache[key] = obj;
            return true;
        }
        /// <summary>
        /// 加入缓存并设置时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public bool Add(string key, object obj, DateTime timeOut)
        {
            System.Web.HttpContext.Current.Cache.Insert(key, obj, null, timeOut, System.Web.Caching.Cache.NoSlidingExpiration);
            return true;
        }
        /// <summary>
        /// 重新添加缓存（清除缓存）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public bool Set(string key, object obj, DateTime timeOut)
        {
            Delete(key);
            System.Web.HttpContext.Current.Cache.Insert(key, obj, null, timeOut, System.Web.Caching.Cache.NoSlidingExpiration);
            return true;
        }
        /// <summary>
        /// 重新添加缓存（清除缓存）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Set(string key, object obj)
        {
            Delete(key);
            System.Web.HttpContext.Current.Cache[key] = obj;
            return true;
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string key)
        {
            System.Web.HttpContext.Current.Cache.Remove(key);
            return true;
        }
    }

    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        object Get(string key);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">键</param>
        ///<returns>值</returns>
        T Get<T>(string key);

        /// <summary>
        /// 添加指定Key的对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="obj">值</param>
        bool Add(string key, object obj);

        /// <summary>
        /// 添加指定Key的对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="obj">值</param>
        /// <param name="TimeOut">到期时间</param>
        bool Add(string key, object obj, DateTime TimeOut);

        /// <summary>
        /// 更新指定Key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Set(string key, object obj);

        /// <summary>
        /// 更新指定Key的对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="TimeOut">到期时间</param>
        /// <returns></returns>
        bool Set(string key, object obj, DateTime TimeOut);

        /// <summary>
        /// 移除指定key的对象
        /// </summary>
        /// <param name="key">键</param>
        bool Delete(string key);
    }
}

