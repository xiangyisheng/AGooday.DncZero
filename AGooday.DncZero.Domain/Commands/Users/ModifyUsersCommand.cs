using AGooday.DncZero.Domain.Communication;
using AGooday.DncZero.Domain.Core.Commands;
using AGooday.DncZero.Domain.Models;
using AGooday.DncZero.Domain.Validations.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Commands.Users
{
    /// <summary>
    /// 注册一个更新 Users 命令
    /// 基础抽象Users命令模型
    /// </summary>
    public class ModifyUsersCommand : Command<Response<Models.Users>>
    {
        // set 受保护，只能通过构造函数方法赋值
        public ModifyUsersCommand(
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

        public Guid Id { get; protected set; }
        /// <summary>
        /// 用户类型(0:系统 1:普通)
        /// </summary>
        public int Type { get; protected set; }
        /// <summary>
        /// 会员类型ID
        /// </summary>
        public Guid? MtypeId { get; protected set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; protected set; }
        /// <summary>
        /// 姓氏
        /// </summary>
        public string Surname { get; protected set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; protected set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; protected set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; protected set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; protected set; }
        /// <summary>
        /// 性别(0:女性 1:男性 2:其他/0: female 1: male 1: other)
        /// </summary>
        public int? Sex { get; protected set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; protected set; }
        /// <summary>
        /// 个人全球统一标识(全称是Globally Recognized Avatar)
        /// </summary>
        public string Gravatar { get; protected set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; protected set; }
        /// <summary>
        /// 座右铭/[signature]
        /// </summary>
        public string Motto { get; protected set; }
        /// <summary>
        /// 自我介绍/个人简历(biography的缩写)
        /// </summary>
        public string Bio { get; protected set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string Idcard { get; protected set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string Major { get; protected set; }
        /// <summary>
        /// 政体(团员/党员)
        /// </summary>
        public string Polity { get; protected set; }
        /// <summary>
        /// 当前登录状态 0:离线 1:在线 2:隐身 3:离开
        /// </summary>
        public int? NowState { get; protected set; }
        /// <summary>
        /// 用户状态 0:未激活 1:激活 2:冻结 3:解冻
        /// </summary>
        public int? State { get; protected set; }
        /// <summary>
        /// 户籍
        /// </summary>
        //public StudentAddress studentAddress { get; protected set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; protected set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; protected set; }
        /// <summary>
        /// 区县
        /// </summary>
        public string County { get; protected set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; protected set; }
        /// <summary>
        /// 详细
        /// </summary>
        public string Detailed { get; protected set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; protected set; }
        /// <summary>
        /// 网站
        /// </summary>
        public string Website { get; protected set; }
        /// <summary>
        /// 微博
        /// </summary>
        public string Weibo { get; protected set; }
        /// <summary>
        /// 博客
        /// </summary>
        public string Blog { get; protected set; }
        /// <summary>
        /// 个性地址
        /// </summary>
        public string Url { get; protected set; }
        /// <summary>
        /// 注册时间/加入时间
        /// </summary>
        public DateTime RegisterTime { get; protected set; }
        /// <summary>
        /// 注册IP
        /// </summary>
        public string RegisterIp { get; protected set; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime LastLoginTime { get; protected set; }
        /// <summary>
        /// 最近登录IP
        /// </summary>
        public string LastLoginIp { get; protected set; }
        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; protected set; }
        /// <summary>
        /// 上次修改IP
        /// </summary>
        public string LastModifiedIp { get; protected set; }
        /// <summary>
        /// 排序
        /// </summary>
        public long? Sort { get; set; }

        public ICollection<UserAuths> UserAuths { get; set; }

        // 重写基类中的 是否有效 方法
        // 主要是为了引入命令验证 UpdateUsersCommandValidation。
        public override bool IsValid()
        {
            ValidationResult = new ModifyUsersCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
