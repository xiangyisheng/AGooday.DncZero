using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class OperateLogs : Entity<Guid>
    {
        public OperateLogs()
        {
            DataLogs = new List<DataLogs>();
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; private set; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; private set; }

        /// <summary>
        /// 数据日志集合
        /// </summary>
        public virtual ICollection<DataLogs> DataLogs { get; set; }
    }
}
