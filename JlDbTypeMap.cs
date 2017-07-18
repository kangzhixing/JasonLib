namespace CodeTool.common
{
    public class JlDbTypeMap
    {
        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="isNullable"></param>
        /// <returns>JAVA类型</returns>
        public static string Map4J(string dbType, bool isNullable = false)
        {
            switch (dbType)
            {
                case "bigint":
                    return isNullable ? "Long" : "long";
                case "int":
                    return isNullable ? "Integer" : "int";
                case "tinyint":
                    return isNullable ? "Short" : "short";
                case "bit":
                    return isNullable ? "Boolean" : "boolean";
                case "smalldatetime":
                case "date":
                case "datetime":
                    return "Date";
                case "numeric":
                case "decimal":
                    return "BigDecimal";
                case "float":
                    return isNullable ? "Double" : "double";
                default:
                    return "String";
            }
        }

        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>.NET类型</returns>
        public static string Map(string dbType, bool isNullable = false)
        {
            var result = string.Empty;
            switch (dbType)
            {
                case "bigint": result = "long"; break;
                case "int":
                case "tinyint": result = "int"; break;
                case "bit": result = "byte"; break;
                case "smalldatetime":
                case "date":
                case "datetime": result = "DateTime"; break;
                case "decimal":
                case "float": result = "decimal"; break;
                default: result = "string"; break;
            }

            return isNullable && result != "string" ? result + "?" : result;
        }
    }
}