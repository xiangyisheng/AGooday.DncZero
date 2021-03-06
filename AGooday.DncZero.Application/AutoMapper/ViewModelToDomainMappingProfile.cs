﻿using AGooday.DncZero.Application.ViewModels;
using AGooday.DncZero.Domain.Commands.Users;
using AGooday.DncZero.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Application.AutoMapper
{
    /// <summary>
    /// 视图模型 -> 领域模式的映射，是 写 命令
    /// </summary>
    public class ViewModelToDomainMappingProfile : Profile
    {

        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public ViewModelToDomainMappingProfile()
        {
            //CreateMap<UsersViewModel, Users>();

            //手动进行配置
            CreateMap<UsersViewModel, Users>()
                .ForPath(d => d.Address.Province, o => o.MapFrom(s => s.Province))
                .ForPath(d => d.Address.City, o => o.MapFrom(s => s.City))
                .ForPath(d => d.Address.County, o => o.MapFrom(s => s.County))
                .ForPath(d => d.Address.Street, o => o.MapFrom(s => s.Street))
                .ForPath(d => d.Address.Detailed, o => o.MapFrom(s => s.Detailed))
                ;

            //这里以后会写领域命令，所以不能和DomainToViewModelMappingProfile写在一起。
            #region Users视图模型 -> 添加新Users命令模型
            //Users视图模型 -> 添加新Users命令模型
            CreateMap<UsersViewModel, CreateUsersCommand>()
                .ConstructUsing(c => new CreateUsersCommand(
                    c.Type,
                    c.MtypeId,
                    c.NickName,
                    c.Surname,
                    c.Name,
                    c.RealName,
                    c.Phone,
                    c.Email,
                    c.BirthDate,
                    c.Sex,
                    c.Age,
                    c.Gravatar,
                    c.Avatar,
                    c.Motto,
                    c.Bio,
                    c.Idcard,
                    c.Major,
                    c.Polity,
                    c.NowState,
                    c.State,
                    c.Province, c.City, c.County, c.Street, c.Detailed,
                    c.Company,
                    c.Website,
                    c.Weibo,
                    c.Blog,
                    c.Url,
                    c.RegisterTime,
                    c.RegisterIp,
                    c.LastLoginTime,
                    c.LastLoginIp,
                    c.LastModifiedTime,
                    c.LastModifiedIp,
                    c.Sort
                    ));
            CreateMap<UsersViewModel, RegisterUsersCommand>()
                .ConstructUsing(c => new RegisterUsersCommand(
                    c.Type,
                    c.MtypeId,
                    c.NickName,
                    c.Surname,
                    c.Name,
                    c.RealName,
                    c.Phone,
                    c.Email,
                    c.BirthDate,
                    c.Sex,
                    c.Age,
                    c.Gravatar,
                    c.Avatar,
                    c.Motto,
                    c.Bio,
                    c.Idcard,
                    c.Major,
                    c.Polity,
                    c.NowState,
                    c.State,
                    c.Province, c.City, c.County, c.Street, c.Detailed,
                    c.Company,
                    c.Website,
                    c.Weibo,
                    c.Blog,
                    c.Url,
                    c.RegisterTime,
                    c.RegisterIp,
                    c.LastLoginTime,
                    c.LastLoginIp,
                    c.LastModifiedTime,
                    c.LastModifiedIp,
                    c.Sort
                    ));
            #endregion

            #region Users视图模型 -> 更新Users信息命令模型
            //Users视图模型 -> 更新Users信息命令模型
            CreateMap<UsersViewModel, UpdateUsersCommand>()
                .ConstructUsing(c => new UpdateUsersCommand(
                    c.Id,
                    c.Type,
                    c.MtypeId,
                    c.NickName,
                    c.Surname,
                    c.Name,
                    c.RealName,
                    c.Phone,
                    c.Email,
                    c.BirthDate,
                    c.Sex,
                    c.Age,
                    c.Gravatar,
                    c.Avatar,
                    c.Motto,
                    c.Bio,
                    c.Idcard,
                    c.Major,
                    c.Polity,
                    c.NowState,
                    c.State,
                    c.Province, c.City, c.County, c.Street, c.Detailed,
                    c.Company,
                    c.Website,
                    c.Weibo,
                    c.Blog,
                    c.Url,
                    c.RegisterTime,
                    c.RegisterIp,
                    c.LastLoginTime,
                    c.LastLoginIp,
                    c.LastModifiedTime,
                    c.LastModifiedIp,
                    c.Sort
                    ));
            CreateMap<UsersViewModel, ModifyUsersCommand>()
                .ConstructUsing(c => new ModifyUsersCommand(
                    c.Id,
                    c.Type,
                    c.MtypeId,
                    c.NickName,
                    c.Surname,
                    c.Name,
                    c.RealName,
                    c.Phone,
                    c.Email,
                    c.BirthDate,
                    c.Sex,
                    c.Age,
                    c.Gravatar,
                    c.Avatar,
                    c.Motto,
                    c.Bio,
                    c.Idcard,
                    c.Major,
                    c.Polity,
                    c.NowState,
                    c.State,
                    c.Province, c.City, c.County, c.Street, c.Detailed,
                    c.Company,
                    c.Website,
                    c.Weibo,
                    c.Blog,
                    c.Url,
                    c.RegisterTime,
                    c.RegisterIp,
                    c.LastLoginTime,
                    c.LastLoginIp,
                    c.LastModifiedTime,
                    c.LastModifiedIp,
                    c.Sort
                    ));
            #endregion

            CreateMap<MenusViewModel, Menus>();

            CreateMap<UserAuthsViewModel, UserAuths>();
        }
    }
}
