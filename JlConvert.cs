﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JasonLib
{
    public class JlConvert
    {
        public static bool TryToBool(object obj, bool defaultValue = false)
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

        /// <summary>
        /// 尝试转换（失败则返回默认值） - 转换为byte类型
        /// </summary>
        /// <param name="obj">输入</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转换后的byte类型（异常则返回0）</returns>
        public static byte TryToByte(object obj, byte defaultValue = 0)
        {
            byte result;
            return byte.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 尝试转换（失败则返回默认值） - 转换为long类型
        /// </summary>
        /// <param name="obj">输入</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转换后的long类型（异常则返回0）</returns>
        public static long TryToLong(object obj, long defaultValue = 0)
        {
            long result;
            return long.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 尝试转换（失败则返回默认值） - 转换为short类型
        /// </summary>
        /// <param name="obj">输入</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转换后的short类型（异常则返回0）</returns>
        public static short TryToShort(object obj, short defaultValue = 0)
        {
            short result;
            return short.TryParse(obj.ToString(), out result) ? result : defaultValue;
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
