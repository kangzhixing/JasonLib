using System.Web.Security;

namespace JasonLib
{
    public class JlMd5
    {
        /// <summary>
        /// 哈希值
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>结果</returns>
        public static string HashPassword(string input)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5");
        }
    }
}
