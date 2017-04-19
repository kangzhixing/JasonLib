using System;

namespace JasonLib
{
    public class JlGuid
    {
        /// <summary>
        /// 产生Guid
        /// </summary>
        /// <returns>小写GUID字符串</returns>
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("N").ToLower();
        }
    }
}
