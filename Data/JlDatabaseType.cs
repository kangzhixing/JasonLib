using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JasonLib.Data
{
    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    public enum JlDatabaseType
    {
        /// <summary>
        /// OleDb数据库
        /// </summary>
        OleDb = 1,

        /// <summary>
        /// SqlServer数据库
        /// </summary>
        SqlServer = 2,

        /// <summary>
        /// SQLite数据库
        /// </summary>
        SQLite = 3,

        /// <summary>
        /// MySql数据库
        /// </summary>
        MySql = 4,

        /// <summary>
        /// PostgreSql数据库
        /// </summary>
        PostgreSql = 5
    }
}
