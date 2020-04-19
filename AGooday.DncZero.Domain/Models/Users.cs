using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class Users : SortableEntity<Guid, long>
    {
        protected Users()
        {
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="type">用户类型(0:系统 1:普通)</param>
        /// <param name="mtypeid">会员类型ID</param>
        /// <param name="nickname">昵称</param>
        /// <param name="surname">姓氏</param>
        /// <param name="name">名字</param>
        /// <param name="realname">真实姓名</param>
        /// <param name="phone">手机号码</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="birthdate">出生日期</param>
        /// <param name="sex">性别(0:女性 1:男性/0: female 1: male)</param>
        /// <param name="age">年龄</param>
        /// <param name="gravatar">个人全球统一标识(全称是Globally Recognized Avatar)</param>
        /// <param name="avatar">头像</param>
        /// <param name="motto">座右铭/[signature]</param>
        /// <param name="bio">自我介绍/个人简历(biography的缩写)</param>
        /// <param name="idcard">身份证号</param>
        /// <param name="major">专业</param>
        /// <param name="polity">政体(团员/党员)</param>
        /// <param name="nowstate">当前登录状态 0:离线 1:在线 2:隐身 3:离开</param>
        /// <param name="state">用户状态 0:未激活 1:激活 2:冻结 3:解冻</param>
        /// <param name="address">公司</param>
        /// <param name="company">网站</param>
        /// <param name="website">微博</param>
        /// <param name="weibo">博客</param>
        /// <param name="blog">个性地址</param>
        /// <param name="url">注册时间/加入时间</param>
        /// <param name="registertime">注册IP</param>
        /// <param name="registerip">最近登录时间</param>
        /// <param name="lastlogintime">最近登录IP</param>
        /// <param name="lastloginip">上次修改时间</param>
        /// <param name="lastmodifiedtime">上次修改IP</param>
        /// <param name="modifiedip">排序</param>
        /// <param name="userauths">用户授权</param>
        /// <param name="sort">
        public Users(
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
            , Address address
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
            , ICollection<UserAuths> userauths
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
            Address = address;
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
            UserAuths = userauths;
            Sort = sort;
        }
        /// <summary>
        /// 用户类型(0:系统 1:普通)
        /// </summary>
        public int Type { get; private set; }
        /// <summary>
        /// 会员类型ID
        /// </summary>
        public Guid? MtypeId { get; private set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; private set; }
        /// <summary>
        /// 姓氏
        /// </summary>
        public string Surname { get; private set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; private set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; private set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; private set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; private set; }
        /// <summary>
        /// 性别(0:女性 1:男性 2:其他/0: female 1: male 1: other)
        /// </summary>
        public int? Sex { get; private set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; private set; }
        /// <summary>
        /// 个人全球统一标识(全称是Globally Recognized Avatar)
        /// </summary>
        public string Gravatar { get; private set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; private set; }
        /// <summary>
        /// 座右铭/[signature]
        /// </summary>
        public string Motto { get; private set; }
        /// <summary>
        /// 自我介绍/个人简历(biography的缩写)
        /// </summary>
        public string Bio { get; private set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string Idcard { get; private set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string Major { get; private set; }
        /// <summary>
        /// 政体(团员/党员)
        /// </summary>
        public string Polity { get; private set; }
        /// <summary>
        /// 当前登录状态 0:离线 1:在线 2:隐身 3:离开
        /// </summary>
        public int? NowState { get; private set; }
        /// <summary>
        /// 用户状态 0:未激活 1:激活 2:冻结 3:解冻
        /// </summary>
        public int? State { get; private set; }
        /// <summary>
        /// 户籍
        /// </summary>
        public Address Address { get; private set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; private set; }
        /// <summary>
        /// 网站
        /// </summary>
        public string Website { get; private set; }
        /// <summary>
        /// 微博
        /// </summary>
        public string Weibo { get; private set; }
        /// <summary>
        /// 博客
        /// </summary>
        public string Blog { get; private set; }
        /// <summary>
        /// 个性地址
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// 注册时间/加入时间
        /// </summary>
        public DateTime RegisterTime { get; private set; }
        /// <summary>
        /// 注册IP
        /// </summary>
        public string RegisterIp { get; private set; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime LastLoginTime { get; private set; }
        /// <summary>
        /// 最近登录IP
        /// </summary>
        public string LastLoginIp { get; private set; }
        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; private set; }
        /// <summary>
        /// 上次修改IP
        /// </summary>
        public string LastModifiedIp { get; private set; }

        /// <summary>
        /// 用户授权详情
        /// </summary>
        public virtual ICollection<UserAuths> UserAuths { get; private set; }
    }
}
