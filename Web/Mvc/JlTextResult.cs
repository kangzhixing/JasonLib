using System.Web.Mvc;

namespace JasonLib.Web.Mvc
{
    public class JlTextResult : ContentResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public JlTextResult()
        {
            ContentType = JlMimeType.Text;
        }
    }
}
