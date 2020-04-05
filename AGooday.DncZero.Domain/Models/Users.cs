using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    public class Users : SortableEntity<long, Guid>
    {
        protected Users()
        {
        }
        public Users(Guid id, string nickname, string surname, string name, string realname, string email, string phone, DateTime? birthDate, Address address)
        {
            Id = id;
            NickName = nickname;
            Surname = surname;
            Name = name;
            RealName = realname;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
            Address = address;
        }
        public string NickName { get; private set; }
        public string Surname { get; private set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; private set; }
        public string RealName { get; private set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; private set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; private set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDate { get; private set; }

        /// <summary>
        /// 户籍
        /// </summary>
        public Address Address { get; private set; }
    }
}
