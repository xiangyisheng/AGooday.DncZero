using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Commands.Users
{
    /// <summary>
    /// 地址
    /// </summary>
    public class StudentAddress
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Street { get; set; }
        public string Detailed { get; set; }
    }
}
