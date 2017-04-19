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
        public static string Map(string dbType)
        {
            switch (dbType)
            {
                case "bigint":
                    return "long";
                case "int":
                case "tinyint":
                    return "int";
                case "bit":
                    return "byte";
                case "smalldatetime":
                case "date":
                case "datetime":
                    return "DateTime";
                case "decimal":
                case "float":
                    return "decimal";
                default:
                    return "string";
            }
        }
    }
}