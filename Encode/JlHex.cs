using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JasonLib.Encode
{
    /// <summary>
    /// 十六进制处理
    /// </summary>
    public static class JlHex
    {

        /// <summary>
        /// 转化为16进制
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        public static char BinaryToHex(int binary)
        {
            char ch;
            switch (binary)
            {
                case 0:
                    ch = '0';
                    break;
                case 1:
                    ch = '1';
                    break;
                case 2:
                    ch = '2';
                    break;
                case 3:
                    ch = '3';
                    break;
                case 4:
                    ch = '4';
                    break;
                case 5:
                    ch = '5';
                    break;
                case 6:
                    ch = '6';
                    break;
                case 7:
                    ch = '7';
                    break;
                case 8:
                    ch = '8';
                    break;
                case 9:
                    ch = '9';
                    break;
                case 10:
                    ch = 'a';
                    break;
                case 11:
                    ch = 'b';
                    break;
                case 12:
                    ch = 'c';
                    break;
                case 13:
                    ch = 'd';
                    break;
                case 14:
                    ch = 'e';
                    break;
                case 15:
                    ch = 'f';
                    break;
                default:
                    ch = ' ';
                    break;
            }
            return ch;
        }

        /// <summary>
        /// 转换为十六进制
        /// </summary>
        /// <param name="datas">数据</param>
        /// <returns>转换的结果</returns>
        public static byte[] ToHex(byte[] datas)
        {
            var hexChars = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

            using (var memoryStream = new MemoryStream(datas.Length * 2))
            {
                foreach (var data in datas)
                {
                    var hexByte = new byte[2];
                    hexByte[0] = (byte)hexChars[(data & 0xF0) >> 4];
                    hexByte[1] = (byte)hexChars[data & 0x0F];
                    memoryStream.Write(hexByte, 0, 2);
                }

                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 转换为十六进制
        /// </summary>
        /// <param name="input">待转换的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>转换的结果</returns>
        public static string ToHex(string input, Encoding encoding = null)
        {
            encoding = encoding ?? JlEncoding.Default;

            var data = encoding.GetBytes(input);
            var hexData = ToHex(data);
            return encoding.GetString(hexData);
        }

        /// <summary>
        /// 由十六进制数据转换为原形式
        /// </summary>
        /// <param name="hexData">十六进制数据</param>
        /// <returns>原形式</returns>
        public static byte[] FromHex(byte[] hexData)
        {
            using (var memoryStream = new MemoryStream(hexData.Length / 2))
            {
                for (var i = 0; i < hexData.Length; i += 2)
                {
                    var hexPairInDecimal = new byte[2];
                    for (var h = 0; h < 2; h++)
                    {
                        var temp = (char)hexData[i + h];
                        if (temp == '0')
                        {
                            hexPairInDecimal[h] = 0;
                        }
                        else if (temp == '1')
                        {
                            hexPairInDecimal[h] = 1;
                        }
                        else if (temp == '2')
                        {
                            hexPairInDecimal[h] = 2;
                        }
                        else if (temp == '3')
                        {
                            hexPairInDecimal[h] = 3;
                        }
                        else if (temp == '4')
                        {
                            hexPairInDecimal[h] = 4;
                        }
                        else if (temp == '5')
                        {
                            hexPairInDecimal[h] = 5;
                        }
                        else if (temp == '6')
                        {
                            hexPairInDecimal[h] = 6;
                        }
                        else if (temp == '7')
                        {
                            hexPairInDecimal[h] = 7;
                        }
                        else if (temp == '8')
                        {
                            hexPairInDecimal[h] = 8;
                        }
                        else if (temp == '9')
                        {
                            hexPairInDecimal[h] = 9;
                        }
                        else if (temp == 'A' || temp == 'a')
                        {
                            hexPairInDecimal[h] = 10;
                        }
                        else if (temp == 'B' || temp == 'b')
                        {
                            hexPairInDecimal[h] = 11;
                        }
                        else if (temp == 'C' || temp == 'c')
                        {
                            hexPairInDecimal[h] = 12;
                        }
                        else if (temp == 'D' || temp == 'd')
                        {
                            hexPairInDecimal[h] = 13;
                        }
                        else if (temp == 'E' || temp == 'e')
                        {
                            hexPairInDecimal[h] = 14;
                        }
                        else if (temp == 'F' || temp == 'f')
                        {
                            hexPairInDecimal[h] = 15;
                        }
                    }
                    memoryStream.WriteByte((byte)((hexPairInDecimal[0] << 4) | hexPairInDecimal[1]));
                }

                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 转换回原形式
        /// </summary>
        /// <param name="input">待转换的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>原形式</returns>
        public static string FromHex(string input, Encoding encoding = null)
        {
            encoding = encoding ?? JlEncoding.Default;
            var hexData = encoding.GetBytes(input);
            var data = FromHex(hexData);
            return encoding.GetString(data);
        }
    }
}
