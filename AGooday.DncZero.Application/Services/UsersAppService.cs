using AGooday.DncZero.Application.EventSourcedNormalizers.Users;
using AGooday.DncZero.Application.Interfaces;
using AGooday.DncZero.Application.ViewModels;
using AGooday.DncZero.Common.Enumerator;
using AGooday.DncZero.Common.Extensions;
using AGooday.DncZero.Domain.Commands.Users;
using AGooday.DncZero.Domain.Communication;
using AGooday.DncZero.Domain.Core.Bus;
using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Domain.Models;
using AGooday.DncZero.Domain.Queries;
using AGooday.DncZero.Domain.Queries.Users;
using AGooday.DncZero.Infrastructure.Repository.EventSourcing;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly MediatR.IMediator _mediator;
        // 事件源仓储
        private readonly IEventStoreRepository _eventStoreRepository;

        public UsersAppService(
            IUsersRepository usersRepository,
            IMapper mapper,
            IMediatorHandler bus,
            MediatR.IMediator mediator,
            IEventStoreRepository eventStoreRepository
            )
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            Bus = bus;
            _mediator = mediator;
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
        public async Task<IEnumerable<UsersViewModel>> ListAsync()
        {
            var user = await Bus.SendQuery(new UserListQuery());
            return _mapper.Map<IEnumerable<UsersViewModel>>(user);
        }
        public UsersViewModel GetById(Guid id)
        {
            return _mapper.Map<UsersViewModel>(_usersRepository.GetById(id));
        }

        public async Task<UsersViewModel> FindByIdAsync(Guid id)
        {
            var query = new GetByIdQuery<Users, Guid>(id);
            //var user = await _mediator.Send(new GetByIdQuery<Users>(id));
            //var user = await Bus.SendQuery<GetByIdQuery<Users>>(new GetByIdQuery<Users>(id));
            var user = await Bus.SendQuery(new GetByIdQuery<Users, Guid>(id));
            return _mapper.Map<UsersViewModel>(user);
        }

        public void Update(UsersViewModel usersViewModel)
        {
            var updateCommand = _mapper.Map<UpdateUsersCommand>(usersViewModel);
            Bus.SendCommand(updateCommand);
        }
        public async Task<Response<Users>> ModifyAsync(UsersViewModel usersViewModel)
        {
            var modifyCommand = _mapper.Map<ModifyUsersCommand>(usersViewModel);
            //var user = await _mediator.Send(modifyCommand);
            var user = await Bus.SendCommandAsync(modifyCommand);
            return (Response<Users>)user;
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
        /// 登录
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns></returns>
        public LoginResultViewModel Login(LoginViewModel loginViewModel)
        {
            var result = new LoginResultViewModel();
            var entity = _usersRepository.Login(loginViewModel.Identifier, loginViewModel.Credential.ToMd5());
            result.Identifier = loginViewModel.Identifier;
            result.User = _mapper.Map<UsersViewModel>(entity);
            if (entity == null)
            {
                result.LoginSuccess = false;
                result.Message = "Account or password wrong ";
                result.Result = LoginResult.AccountOrPasswordWrong;
            }
            else
            {
                result.LoginSuccess = true;
            }
            var loginLog = new LoginLogs
            {
                UserId = entity?.Id,
                LoginName = entity.Name,
                IP = loginViewModel.IP,
                LoginTime = DateTime.Now,
                Message = result.Message
            };
            //增加日志
            return result;
        }
        public async Task<Response<Users>> LoginAsync(LoginViewModel loginViewModel)
        {
            var user = await Bus.SendQuery(new UserLoginQuery(loginViewModel.Identifier, loginViewModel.Credential.ToMd5()));
            return (Response<Users>)user;
        }

        public void Create(UsersViewModel usersViewModel)
        {
            //这里引入领域设计中的写命令 还没有实现
            //请注意这里如果是平时的写法，必须要引入 Users 领域模型，会造成污染

            //_UsersRepository.Add(_mapper.Map<Users>(UsersViewModel));
            //_UsersRepository.SaveChanges();
            var sort = usersViewModel.Sort;
            usersViewModel.Sort = sort == null ? 1 : sort;
            var createCommand = _mapper.Map<CreateUsersCommand>(usersViewModel);
            Bus.SendCommand(createCommand);
        }
        //public void Register(UsersViewModel UsersViewModel, RegisterViewModel registerViewModel)
        //{
        //    //这里引入领域设计中的写命令 还没有实现
        //    //请注意这里如果是平时的写法，必须要引入 Users 领域模型，会造成污染

        //    //_UsersRepository.Add(_mapper.Map<Users>(UsersViewModel));
        //    //_UsersRepository.SaveChanges();
        //    var sort = UsersViewModel.Sort;
        //    UsersViewModel.Sort = sort == null ? 1 : sort;

        //    var registerCommand = _mapper.Map<RegisterUsersCommand>(UsersViewModel);

        //    var userauth = new UserAuths(Guid.NewGuid())
        //    {
        //        IdentityType = "email",
        //        Identifier = registerViewModel.Identifier,
        //        Credential = registerViewModel.Credential.ToMd5(),
        //        State = 1,
        //        AuthTime = DateTime.Now,
        //        LastModifiedTime = DateTime.Now,
        //        Verified = true,
        //    };
        //    registerCommand.UserAuths.Add(userauth);

        //    Bus.SendCommand(registerCommand);
        //}
        public async Task<Response<Users>> RegisterAsync(UsersViewModel usersViewModel)
        {
            var registerCommand = _mapper.Map<RegisterUsersCommand>(usersViewModel);
            //var user = await _mediator.Send(registerCommand);
            var user = await Bus.SendCommandAsync(registerCommand);
            return (Response<Users>)user;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="RegisterViewModel">注册信息</param>
        /// <returns></returns>
        public async Task<UsersViewModel> RegisterAsync(RegisterViewModel registerViewModel)
        {
            var usermodel = new UsersViewModel()
            {
                Id = Guid.NewGuid(),
                Sort = 0,
                Type = 0,
                NickName = registerViewModel.NickName,
                Email = registerViewModel.Identifier,
                RegisterTime = DateTime.Now,
                RegisterIp = registerViewModel.IP
            };
            var userauth = new UserAuths(Guid.NewGuid())
            {
                UserId = usermodel.Id,
                IdentityType = "email",
                Identifier = registerViewModel.Identifier,
                Credential = registerViewModel.Credential.ToMd5(),
                State = 1,
                AuthTime = DateTime.Now,
                LastModifiedTime = DateTime.Now,
                Verified = true,
            };
            var user = _mapper.Map<Users>(usermodel);
            var entity = await _usersRepository.RegisterAsync(user, userauth);
            var result = _mapper.Map<UsersViewModel>(entity);
            return result;
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
            return _mapper.Map<IList<MenusViewModel>>(_usersRepository.GetAllMenus(userId)); ;
        }
    }
}
