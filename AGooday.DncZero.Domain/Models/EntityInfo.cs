using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    public class EntityInfo
    {
        /// <summary>
        /// 实体数据类型名称
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 实体类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 获取 是否启用数据日志
        /// </summary>
        public bool DataLogEnabled { get; }

        /// <summary>
        /// 获取 实体属性信息字典
        /// </summary>
        public IDictionary<string, string> PropertyNames { get; }
    }
}
