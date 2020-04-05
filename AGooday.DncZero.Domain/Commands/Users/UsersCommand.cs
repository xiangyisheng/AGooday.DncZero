using AGooday.DncZero.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Commands.Users
{
    public abstract class UsersCommand : Command
    {
        public Guid Id { get; protected set; }
        public string NickName { get; protected set; }
        /// <summary>
        /// 姓氏
        /// </summary>
        public string Surname { get; protected set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; protected set; }
        public string RealName { get; protected set; }

        public string Email { get; protected set; }

        public DateTime? BirthDate { get; protected set; }

        public string Phone { get; protected set; }

        //public StudentAddress studentAddress { get; protected set; }
        public string Province { get; protected set; }
        public string City { get; protected set; }
        public string County { get; protected set; }
        public string Street { get; protected set; }

    }
}
