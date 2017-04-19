namespace CodeTool.common
{
    public class JlString
    {
        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="isNullable"></param>
        /// <returns>结果</returns>
        public static string ToLowerFirst(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;
            if (str.Length == 1) return str.ToLower();
            return str.Substring(0, 1).ToLower() + str.Substring(1);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="isNullable"></param>
        /// <returns>结果</returns>
        public static string ToUpperFirst(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;
            if (str.Length == 1) return str.ToUpper();
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        /// <summary>
        /// 截取左半部分
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="length">长度</param>
        /// <param name="ex">扩展字符</param>
        /// <returns>结果</returns>
        public static string Left(string input, int length, string ex = "...")
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }
            if (input.Length > length)
            {
                return input.Substring(0, length) + ex;
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// 获取字符串相似度
        /// 定义相似度 = (较长的字符穿的长度-编辑距离) / 较长的字符穿的长度
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns>相似度</returns>
        public static float GetSimilarity(string strA, string strB)
        {
            int editDistance = GetEditDistance(strA, strB);
            if (strA.Length > strB.Length) return (float)(strA.Length - editDistance) / strA.Length;
            return (float)(strB.Length - editDistance) / strB.Length;
        }

        /// <summary>
        /// 获取字符串相似度
        /// 定义相似度 = (较长的字符穿的长度-编辑距离) / 较长的字符穿的长度
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns>相似度</returns>
        public static float GetSimilarityPrecent(string strA, string strB)
        {
            if (strA.Length <= strB.Length)
            {
                return GetSimilarity(strA, strB);
            }
            else
            {
                return GetSimilarity(strB, strA);
            }
        }

        /// <summary>
        /// 计算字符串编辑距离
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static int GetEditDistance(string strA, string strB)
        {
            int lenA = strA.Length;
            int lenB = strB.Length;
            int[,] c = new int[lenA + 1, lenB + 1];


            for (int i = 1; i <= lenA; i++) c[i, 0] = i;
            for (int j = 1; j <= lenB; j++) c[0, j] = j;

            for (int i = 1; i <= lenA; i++)
            {
                for (int j = 1; j <= lenB; j++)
                {
                    if (strB[j - 1] == strA[i - 1])
                    {
                        c[i, j] = c[i - 1, j - 1];
                    }
                    else
                    {
                        int min = c[i - 1, j - 1];
                        if (c[i, j - 1] < min) min = c[i, j - 1];
                        if (c[i - 1, j] < min) min = c[i - 1, j];

                        c[i, j] = min + 1;
                    }
                }
            }
            return c[lenA, lenB];
        }
    }
}