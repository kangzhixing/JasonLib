using System.Web.Mvc;

namespace JasonLib.Web.Mvc
{
    public class JlXmlResult : ContentResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public JlXmlResult()
        {
            ContentType = JlMimeType.Xml;
        }
    }
}
