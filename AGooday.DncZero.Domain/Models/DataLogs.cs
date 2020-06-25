using AGooday.DncZero.Common.Enumerator;
using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 数据日志
    /// </summary>
    public class DataLogs : Entity<Guid>
    {
        public DataLogs()
           : this(null, null, OperatingType.Query)
        { }
        public DataLogs(string name, string entityName, OperatingType type)
        {
            Name = name;
            Type = type;
            EntityName = entityName;
            //LogItems = new List<DataLogItems>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 实体
        /// </summary>
        public string Entity { get; set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }
        /// <summary>
        /// 实体主键值
        /// </summary>
        public string EntityKey { get; set; }
        /// <summary>
        /// 操作类型（0：查询[Query] 10：新建[Insert] 20：更新[Update] 30：删除[Delete]）
        /// </summary>
        //public int OperateType { get; private set; }
        public OperatingType Type { get; set; }
        /// <summary>
        /// 操作日志ID
        /// </summary>
        public Guid OperateLogId { get; set; }

        /// <summary>
        /// 操作日志信息
        /// </summary>
        public virtual OperateLogs OperateLog { get; set; }
        /// <summary>
        /// 操作明细
        /// </summary>
        public virtual ICollection<DataLogItems> LogItems { get; set; } = new List<DataLogItems>();
    }
}
