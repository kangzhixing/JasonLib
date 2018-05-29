using JasonLib.Data;

namespace CodeTool.common
{
    public class JlDbTypeMap
    {
        public static string Map4J(string dbType, bool isNullable = false, JlDatabaseType databaseType = JlDatabaseType.MySql)
        {
            switch (databaseType)
            {
                case JlDatabaseType.MySql:
                    return Map4J_Mysql(dbType, isNullable);

                case JlDatabaseType.PostgreSql:
                    return Map4J_PostgreSql(dbType, isNullable);

                default:
                    return Map4J_Mysql(dbType, isNullable);
            }

        }
        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="dbType">数据类型</param>
        /// <param name="isNullable"></param>
        /// <returns>JAVA类型</returns>
        public static string Map4J_Mysql(string dbType, bool isNullable = false)
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
                case "timestamp":
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
        /// <param name="isNullable"></param>
        /// <returns>JAVA类型</returns>
        public static string Map4J_PostgreSql(string dbType, bool isNullable = false)
        {
            switch (dbType)
            {
                case "bigint":
                    return isNullable ? "Long" : "long";
                case "integer":
                    return isNullable ? "Integer" : "int";
                case "boolean":
                    return isNullable ? "Boolean" : "boolean";
                case "date":
                case "timestamp with time zone":
                case "timestamp without time zone":
                    return "Date";
                case "numeric":
                    return "BigDecimal";
                case "double precision":
                    return isNullable ? "Double" : "double";
                default:
                    return "String";
            }
        }
        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>Mybatis JdbcType类型</returns>
        public static string Map4Mybatis(string dbType)
        {
            switch (dbType)
            {
                case "bigint":
                    return "bigint";
                case "bit":
                    return "bit";
                case "int":
                    return "integer";
                case "tinyint":
                    return "tinyint";
                case "date":
                    return "date";
                case "datetime":
                case "timestamp":
                    return "timestamp";
                case "numeric":
                    return "numeric";
                case "decimal":
                    return "decimal";
                case "float":
                case "double":
                    return "double";
                case "text":
                case "varchar":
                    return "varchar";
                case "nvarchar":
                    return "nvarchar";
                default:
                    return "varchar";
            }
        }

        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>.NET类型</returns>
        public static string Map4CSharp(string dbType, bool isNullable = false)
        {
            var result = string.Empty;
            switch (dbType)
            {
                case "bigint":
                    return isNullable ? "long?" : "long";
                case "int":
                case "tinyint":
                    return isNullable ? "int?" : "int";
                case "bit":
                    return isNullable ? "byte?" : "byte";
                case "smalldatetime":
                case "date":
                case "datetime":
                case "timestamp":
                    return isNullable ? "DateTime?" : "DateTime";
                case "decimal":
                case "float":
                    return isNullable ? "decimal?" : "decimal";
                default:
                    return "string";
            }
        }
    }
}