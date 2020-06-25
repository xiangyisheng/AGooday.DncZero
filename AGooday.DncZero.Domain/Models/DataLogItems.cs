using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 操作日志明细
    /// </summary>
    public class DataLogItems : Entity<Guid>
    {
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

        /// <summary>
        /// 所属数据日志
        /// </summary>
        public virtual DataLogs DataLog { get; set; }
    }
}
