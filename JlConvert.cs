using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JasonLib
{
    public class JlConvert
    {
        public static bool TryToBoolean(object obj, bool defaultValue = false)
        {
            bool result;
            return bool.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 尝试转换（失败则返回默认值） - 转换为Int32类型
        /// </summary>
        /// <param name="obj">输入</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转换后的Int32类型（异常则返回0）</returns>
        public static int TryToInt(object obj, int defaultValue = 0)
        {
            int result;
            return int.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        public static double TryToDouble(object obj, double defaultValue = 0.00)
        {
            double result;

            return double.TryParse(obj.ToString(), out result) ? result : defaultValue;

        }

        public static decimal TryToDecimal(object obj, decimal defaultValue = 0)
        {
            decimal result;

            return decimal.TryParse(obj.ToString(), out result) ? result : defaultValue;
            
        }

        public static DateTime? TryToDateTimeOrNull(object obj)
        {
            DateTime result;

            if (DateTime.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return null;
        }

        public static DateTime TryToDateTime(object obj)
        {
            DateTime result;

            DateTime.TryParse(obj.ToString(), out result);
            return result;
        }
    }
}
