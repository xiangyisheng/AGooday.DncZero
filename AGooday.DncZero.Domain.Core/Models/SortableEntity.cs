using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Core.Models
{
    /// <summary>
    /// 定义可排序领域实体基类
    /// </summary>
    public abstract class SortableEntity<TPrimaryKey, TSort> : Entity<TPrimaryKey>
        where TPrimaryKey : struct
        where TSort : struct
    {
        /// <summary>
        /// 排序号
        /// </summary>
        //public TSort? Sort { get; set; }
        public Nullable<TSort> Sort { get; set; }
    }
}
