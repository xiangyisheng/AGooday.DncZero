using AGooday.DncZero.Application.EventSourcedNormalizers.Users;
using AGooday.DncZero.Application.Interfaces;
using AGooday.DncZero.Application.ViewModels;
using AGooday.DncZero.Domain.Commands.Users;
using AGooday.DncZero.Domain.Core.Bus;
using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Domain.Models;
using AGooday.DncZero.Infrastructure.Repository.EventSourcing;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGooday.DncZero.Application.Services
{
    /// <summary>
    /// GroupsAppService 服务接口实现类,继承 服务接口
    /// 通过 DTO 实现视图模型和领域模型的关系处理
    /// 作为调度者，协调领域层和基础层，
    /// 这里只是做一个面向用户用例的服务接口,不包含业务规则或者知识
    /// </summary>
    public class GroupsAppService : IGroupsAppService
    {
        // 注意这里是要IoC依赖注入的，还没有实现
        private readonly IGroupsRepository _groupsRepository;
        // 用来进行DTO
        private readonly IMapper _mapper;

        public GroupsAppService(
            IGroupsRepository groupsRepository,
            IMapper mapper
            )
        {
            _groupsRepository = groupsRepository;
            _mapper = mapper;
        }
        public IEnumerable<GroupsViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<GroupsViewModel>>(_groupsRepository.GetAll());
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
