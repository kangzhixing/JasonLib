using System.Web.Mvc;

namespace JasonLib.Web.Mvc
{
    public class JlJsonResult : ContentResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public JlJsonResult()
        {
            ContentType = JlMimeType.Json;
        }
    }
}
