using AGooday.DncZero.Domain.Validations.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Commands.Users
{
    /// <summary>
    /// 注册一个添加 Users 命令
    /// 基础抽象学生命令模型
    /// </summary>
    public class RegisterUsersCommand : UsersCommand
    {
        // set 受保护，只能通过构造函数方法赋值
        public RegisterUsersCommand(string nickname, string surname, string name, string realname, string email, DateTime? birthDate, string phone, string province, string city, string county, string street)
        {
            NickName = nickname;
            Surname = surname;
            Name = name;
            RealName = realname;
            Email = email;
            BirthDate = birthDate;
            Phone = phone;
            Province = province;
            City = city;
            County = county;
            Street = street;
        }

        // 重写基类中的 是否有效 方法
        // 主要是为了引入命令验证 RegisterUsersCommandValidation。
        public override bool IsValid()
        {
            ValidationResult = new RegisterUsersCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
