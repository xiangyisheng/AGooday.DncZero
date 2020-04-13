using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 数据日志
    /// </summary>
    public class DataLogs : Entity<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 实体
        /// </summary>
        public string Entity { get; private set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; private set; }
        /// <summary>
        /// 实体主键值
        /// </summary>
        public string EntityKey { get; private set; }
        /// <summary>
        /// 操作类型（0：查询[Query] 10：新建[Insert] 20：更新[Update] 30：删除[Delete]）
        /// </summary>
        public string OperateType { get; private set; }
        /// <summary>
        /// 操作日志ID
        /// </summary>
        public Guid OperateLogId { get; private set; }
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; private set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; private set; }
        /// <summary>
        /// 原值
        /// </summary>
        public string OriginalValue { get; private set; }
        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; private set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; private set; }
    }
}
