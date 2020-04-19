using System;
using System.Collections.Generic;
using System.Text;
using AGooday.DncZero.Application.ViewModels;
using AGooday.DncZero.Domain.Models;
using AutoMapper;

namespace AGooday.DncZero.Application.AutoMapper
{
    /// <summary>
    /// 领域模型 -> 视图模型的映射，是 读 命令
    /// </summary>
    public class DomainToViewModelMappingProfile: Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            //CreateMap<Users, UsersViewModel>();

            CreateMap<Users, UsersViewModel>()
                .ForMember(d => d.County, o => o.MapFrom(s => s.Address.County))
                .ForMember(d => d.Province, o => o.MapFrom(s => s.Address.Province))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(d => d.Detailed, o => o.MapFrom(s => s.Address.Detailed))
                ;

            CreateMap<Menus, MenusViewModel>();
        }
    }
}
