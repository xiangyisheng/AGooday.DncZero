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
    /// MenusAppService 服务接口实现类,继承 服务接口
    /// 通过 DTO 实现视图模型和领域模型的关系处理
    /// 作为调度者，协调领域层和基础层，
    /// 这里只是做一个面向用户用例的服务接口,不包含业务规则或者知识
    /// </summary>
    public class RolesAppService : IRolesAppService
    {
        // 注意这里是要IoC依赖注入的，还没有实现
        private readonly IRolesRepository _rolesRepository;
        // 用来进行DTO
        private readonly IMapper _mapper;

        public RolesAppService(
            IRolesRepository rolesRepository,
            IMapper mapper
            )
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }
        public IEnumerable<RolesViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<RolesViewModel>>(_rolesRepository.GetAll());
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
