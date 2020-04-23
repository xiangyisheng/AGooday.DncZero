using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    public class UserPermissions
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; private set; }
        public Guid MenuId { get; private set; }
        public Guid FunctionId { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Area { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public string Url { get; private set; }
    }
}
