using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Application.EventSourcedNormalizers
{
    /// <summary>
    /// 历史记录模型基类
    /// </summary>
    public abstract class HistoryData
    {
        public string Action { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}
