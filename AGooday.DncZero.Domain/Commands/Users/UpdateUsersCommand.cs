using AGooday.DncZero.Domain.Validations.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Commands.Users
{
    /// <summary>
    /// 注册一个删除 Users 命令
    /// 基础抽象Users命令模型
    /// </summary>
    public class UpdateUsersCommand : UsersCommand
    {
        // set 受保护，只能通过构造函数方法赋值
        public UpdateUsersCommand(
              Guid id
            , int type
            , Guid? mtypeid
            , string nickname
            , string surname
            , string name
            , string realname
            , string phone
            , string email
            , DateTime? birthdate
            , int? sex
            , int? age
            , string gravatar
            , string avatar
            , string motto
            , string bio
            , string idcard
            , string major
            , string polity
            , int? nowstate
            , int? state
            , string province, string city, string county, string street, string detailed
            , string company
            , string website
            , string weibo
            , string blog
            , string url
            , DateTime registertime
            , string registerip
            , DateTime lastlogintime
            , string lastloginip
            , DateTime lastmodifiedtime
            , string lastmodifiedip
            , long? sort
            )
        {
            Id = id;
            Type = type;
            MtypeId = mtypeid;
            NickName = nickname;
            Surname = surname;
            Name = name;
            RealName = realname;
            Phone = phone;
            Email = email;
            BirthDate = birthdate;
            Sex = sex;
            Age = age;
            Gravatar = gravatar;
            Avatar = avatar;
            Motto = motto;
            Bio = bio;
            Idcard = idcard;
            Major = major;
            Polity = polity;
            NowState = nowstate;
            State = state;
            Province = province; City = city; County = county; Street = street; Detailed = detailed;
            Company = company;
            Website = website;
            Weibo = weibo;
            Blog = blog;
            Url = url;
            RegisterTime = registertime;
            RegisterIp = registerip;
            LastLoginTime = lastlogintime;
            LastLoginIp = lastloginip;
            LastModifiedTime = lastmodifiedtime;
            LastModifiedIp = lastmodifiedip;
            Sort = sort;
        }

        // 重写基类中的 是否有效 方法
        // 主要是为了引入命令验证 UpdateUsersCommandValidation。
        public override bool IsValid()
        {
            ValidationResult = new UpdateUsersCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
