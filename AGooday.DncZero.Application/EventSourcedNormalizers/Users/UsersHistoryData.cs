using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Application.EventSourcedNormalizers.Users
{
    /// <summary>
    /// Users 历史记录模型
    /// 用事件溯源
    /// </summary>
    public class UsersHistoryData : HistoryData
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string RealName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
    }
}
