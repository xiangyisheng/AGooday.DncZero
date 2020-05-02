using AGooday.DncZero.Domain.Core.Events;
using AGooday.DncZero.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Events.Users
{
    public class UsersCreatedEvent : UsersEvent
    {
        public UsersCreatedEvent(
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
            , ICollection<UserAuths> userauths
            , long? sort
            )
        {
            AggregateId = id;

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
            UserAuths = userauths;
            Sort = sort;
        }
    }
}
