namespace CodeTool.common
{
    public class JlFieldDescription
    {

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否可为Null
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 是否为自增列
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 主外键情况
        /// </summary>
        public string ColumnKey { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int? Length { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段名(去除下划线)
        /// </summary>
        public string SimpleName
        {
            get
            {
                return JlString.ReplaceUnderline(Name);
            }
            set { }
        }
    }
}