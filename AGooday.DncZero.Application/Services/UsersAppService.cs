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
    /// UsersAppService 服务接口实现类,继承 服务接口
    /// 通过 DTO 实现视图模型和领域模型的关系处理
    /// 作为调度者，协调领域层和基础层，
    /// 这里只是做一个面向用户用例的服务接口,不包含业务规则或者知识
    /// </summary>
    public class UsersAppService : IUsersAppService
    {
        // 注意这里是要IoC依赖注入的，还没有实现
        private readonly IUsersRepository _usersRepository;
        // 用来进行DTO
        private readonly IMapper _mapper;
        // 中介者 总线
        private readonly IMediatorHandler Bus;
        // 事件源仓储
        private readonly IEventStoreRepository _eventStoreRepository;

        public UsersAppService(
            IUsersRepository usersRepository,
            IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository
            )
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<UsersViewModel> GetAll()
        {
            //第一种写法 Map
            return _mapper.Map<IEnumerable<UsersViewModel>>(_usersRepository.GetAll());

            //第二种写法 ProjectTo
            //return (_UsersRepository.GetAll()).ProjectTo<UsersViewModel>(_mapper.ConfigurationProvider);
        }

        public UsersViewModel GetById(Guid id)
        {
            return _mapper.Map<UsersViewModel>(_usersRepository.GetById(id));
        }

        public void Register(UsersViewModel UsersViewModel)
        {
            //这里引入领域设计中的写命令 还没有实现
            //请注意这里如果是平时的写法，必须要引入 Users 领域模型，会造成污染

            //_UsersRepository.Add(_mapper.Map<Users>(UsersViewModel));
            //_UsersRepository.SaveChanges();
            var sort = UsersViewModel.Sort;
            UsersViewModel.Sort = sort == null ? 1 : sort;
            var registerCommand = _mapper.Map<RegisterUsersCommand>(UsersViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(UsersViewModel UsersViewModel)
        {
            var updateCommand = _mapper.Map<UpdateUsersCommand>(UsersViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveUsersCommand(id);
            Bus.SendCommand(removeCommand);
        }

        /// <summary>
        /// 获取某一个聚合id下的所有事件，也就是得到了历史记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<UsersHistoryData> GetAllHistory(Guid id)
        {
            return UsersHistory.ToJavaScriptStudentHistory(_eventStoreRepository.All(id));
        }

        /// <summary>
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
        public bool HasMenusAuthority(Guid userId, string url)
        {
            return _usersRepository.HasMenusAuthority(userId, url);
        }

        public IList<MenusViewModel> GetAllMenus(Guid userId)
        {
            return _mapper.Map<IList<MenusViewModel>>(_usersRepository.GetAllMenus(userId));;
        }
    }
}
