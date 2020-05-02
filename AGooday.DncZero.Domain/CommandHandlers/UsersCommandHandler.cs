using AGooday.DncZero.Common.Extensions;
using AGooday.DncZero.Common.Helper;
using AGooday.DncZero.Domain.Commands.Users;
using AGooday.DncZero.Domain.Communication;
using AGooday.DncZero.Domain.Core.Bus;
using AGooday.DncZero.Domain.Core.Notifications;
using AGooday.DncZero.Domain.Events.Users;
using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AGooday.DncZero.Domain.CommandHandlers
{
    /// <summary>
    /// Users领域命令处理程序
    /// 
    /// IRequestHandler 也是通过结构类型Unit来处理不需要返回值的情况。
    /// </summary>
    public class UsersCommandHandler : CommandHandler,
        IRequestHandler<CreateUsersCommand, Unit>,
        IRequestHandler<RegisterUsersCommand, Response<Users>>,
        IRequestHandler<UpdateUsersCommand, Unit>,
        IRequestHandler<ModifyUsersCommand, Response<Users>>,
        IRequestHandler<RemoveUsersCommand, Unit>
    {
        // 注入仓储接口
        private readonly IUsersRepository _UsersRepository;
        private readonly IUserAuthsRepository _UserAuthsRepository;
        // 注入总线
        private readonly IMediatorHandler Bus;
        private IMemoryCache Cache;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="OrderRepository"></param>
        /// <param name="uow"></param>
        /// <param name="bus"></param>
        /// <param name="cache"></param>
        public UsersCommandHandler(IUsersRepository usersRepository,
            IUserAuthsRepository userAuthsRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            IMemoryCache cache
            ) : base(uow, bus, cache)
        {
            _UsersRepository = usersRepository;
            _UserAuthsRepository = userAuthsRepository;
            Bus = bus;
            Cache = cache;
        }

        // RegisterUsersCommand命令的处理程序
        // 整个命令处理程序的核心都在这里
        // 不仅包括命令验证的收集，持久化，还有领域事件和通知的添加
        public async Task<Response<Users>> Handle(RegisterUsersCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                //return new Response<Users>("Validation failed.");
                return await Task.FromResult(new Response<Users>("Validation failed."));
            }
            #region 
            var user = new Users();
            var address = new Address(request.Province, request.City, request.County, request.Street, request.Detailed);
            user.Type = request.Type;
            user.MtypeId = request.MtypeId;
            user.NickName = request.NickName;
            user.Surname = request.Surname;
            user.Name = request.Name;
            user.RealName = request.RealName;
            user.Phone = request.Phone;
            user.Email = request.Email;
            user.BirthDate = request.BirthDate;
            user.Sex = request.Sex;
            user.Age = request.Age;
            user.Gravatar = request.Gravatar;
            user.Avatar = request.Avatar;
            user.Motto = request.Motto;
            user.Bio = request.Bio;
            user.Idcard = request.Idcard;
            user.Major = request.Major;
            user.Polity = request.Polity;
            user.NowState = request.NowState;
            user.State = request.State;
            user.Address = address;
            user.Company = request.Company;
            user.Website = request.Website;
            user.Weibo = request.Weibo;
            user.Blog = request.Blog;
            user.Url = request.Url;
            user.RegisterTime = request.RegisterTime;
            user.RegisterIp = request.RegisterIp;
            user.LastLoginTime = request.LastLoginTime;
            user.LastLoginIp = request.LastLoginIp;
            user.LastModifiedTime = request.LastModifiedTime;
            user.LastModifiedIp = request.LastModifiedIp;
            user.UserAuths = request.UserAuths;
            user.Sort = request.Sort;
            //user.IsSuperMan = request.IsSuperMan; 
            #endregion

            #region 检查
            //// 判断用户名是否存在
            //// 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            //var existingUsers = _UsersRepository.GetByName(users.Name);
            //if (existingUsers != null && existingUsers.Id != users.Id)
            //{
            //    //引发错误事件
            //    Bus.RaiseEvent(new DomainNotification("", "该用户名已经被使用！"));
            //    return Task.FromResult(new Unit());
            //}
            // 判断邮箱是否存在
            var existingUsers = _UsersRepository.GetByEmail(user.Email);
            if (existingUsers != null && existingUsers.Id != user.Id)
            {
                if (!existingUsers.Equals(user))
                {
                    await Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                    return await Task.FromResult(new Response<Users>("该邮箱已经被使用."));
                }
            }
            #endregion

            await _UsersRepository.AddAsync(user);
            //var result = await CommitAsync();
            if (await CommitAsync())
            {
                #region 
                var usersregisteredevent = new UsersRegisteredEvent(
                            user.Id,
                            user.Type,
                            user.MtypeId,
                            user.NickName,
                            user.Surname,
                            user.Name,
                            user.RealName,
                            user.Phone,
                            user.Email,
                            user.BirthDate,
                            user.Sex,
                            user.Age,
                            user.Gravatar,
                            user.Avatar,
                            user.Motto,
                            user.Bio,
                            user.Idcard,
                            user.Major,
                            user.Polity,
                            user.NowState,
                            user.State,
                            user.Address.Province, user.Address.City, user.Address.County, user.Address.Street, user.Address.Detailed,
                            user.Company,
                            user.Website,
                            user.Weibo,
                            user.Blog,
                            user.Url,
                            user.RegisterTime,
                            user.RegisterIp,
                            user.LastLoginTime,
                            user.LastLoginIp,
                            user.LastModifiedTime,
                            user.LastModifiedIp,
                            user.UserAuths,
                            user.Sort
                            );
                #endregion
                await Bus.RaiseEvent(usersregisteredevent);
            }

            //return new Response<Users>(user);
            return await Task.FromResult(new Response<Users>(user));
        }
        public Task<Unit> Handle(CreateUsersCommand message, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!message.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(message);
                // 返回，结束当前线程
                return Task.FromResult(new Unit());
            }

            var ip = string.Empty;
            SystemHelper.GetLocalIP(ref ip);

            // 实例化领域模型，这里才真正的用到了领域模型
            // 注意这里是通过构造函数方法实现
            var address = new Address(message.Province, message.City, message.County, message.Street, message.Detailed);
            #region 
            var user = new Users(
                    Guid.NewGuid(),
                    message.Type,
                    message.MtypeId,
                    message.NickName,
                    message.Surname,
                    message.Name,
                    message.RealName,
                    message.Phone,
                    message.Email,
                    message.BirthDate,
                    message.Sex,
                    message.Age,
                    message.Gravatar,
                    message.Avatar,
                    message.Motto,
                    message.Bio,
                    message.Idcard,
                    message.Major,
                    message.Polity,
                    message.NowState,
                    message.State,
                    address,
                    message.Company,
                    message.Website,
                    message.Weibo,
                    message.Blog,
                    message.Url,
                    DateTime.Now,//message.RegisterTime,
                    ip,//message.RegisterIp,
                    DateTime.Now,//message.LastLoginTime,
                    ip,//message.LastLoginIp,
                    DateTime.Now,//message.LastModifiedTime,
                    ip,//message.LastModifiedIp,
                    message.UserAuths,
                    message.Sort
                    );
            #endregion

            #region 检查
            //// 判断用户名是否存在
            //// 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            //var existingUsers = _UsersRepository.GetByName(users.Name);
            //if (existingUsers != null && existingUsers.Id != users.Id)
            //{
            //    //引发错误事件
            //    Bus.RaiseEvent(new DomainNotification("", "该用户名已经被使用！"));
            //    return Task.FromResult(new Unit());
            //}
            // 判断邮箱是否存在
            var existingUsers = _UsersRepository.GetByEmail(user.Email);
            if (existingUsers != null && existingUsers.Id != user.Id)
            {
                if (!existingUsers.Equals(user))
                {
                    Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                    return Task.FromResult(new Unit());
                }
            }
            #endregion

            var emailcompose = user.Email.Split("@");
            var password = emailcompose.Length > 0 ? $"{emailcompose[0]}*" : "agooday*";
            user.UserAuths = user.UserAuths != null && user.UserAuths.Count > 0 ? user.UserAuths : new List<UserAuths>() {
                new UserAuths(Guid.NewGuid())
                {
                    UserId = user.Id,
                    IdentityType = "email",
                    Identifier = user.Email,
                    Credential = password.ToMd5(),
                    State = 1,
                    AuthTime = DateTime.Now,
                    LastModifiedTime = DateTime.Now,
                    Verified = true,
                }
            };

            // 持久化
            _UsersRepository.Add(user);

            #region 
            //var emailcompose = user.Email.Split("@");
            //var password = emailcompose.Length > 0 ? $"{emailcompose[0]}*" : "agooday*";
            //var userAuths = user.UserAuths ?? new List<UserAuths>() {
            //    new UserAuths(Guid.NewGuid())
            //    {
            //        UserId = user.Id,
            //        IdentityType = "email",
            //        Identifier = user.Email,
            //        Credential = password.ToMd5(),
            //        State = 1,
            //        AuthTime = DateTime.Now,
            //        LastModifiedTime = DateTime.Now,
            //        Verified = true,
            //    }
            //};
            //foreach (var item in userAuths)
            //{
            //    item.UserId = user.Id;
            //    _UserAuthsRepository.Add(item);
            //} 
            #endregion

            // 统一提交
            if (Commit())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                #region 
                var userscreatedevent = new UsersCreatedEvent(
                            user.Id,
                            user.Type,
                            user.MtypeId,
                            user.NickName,
                            user.Surname,
                            user.Name,
                            user.RealName,
                            user.Phone,
                            user.Email,
                            user.BirthDate,
                            user.Sex,
                            user.Age,
                            user.Gravatar,
                            user.Avatar,
                            user.Motto,
                            user.Bio,
                            user.Idcard,
                            user.Major,
                            user.Polity,
                            user.NowState,
                            user.State,
                            user.Address.Province, user.Address.City, user.Address.County, user.Address.Street, user.Address.Detailed,
                            user.Company,
                            user.Website,
                            user.Weibo,
                            user.Blog,
                            user.Url,
                            user.RegisterTime,
                            user.RegisterIp,
                            user.LastLoginTime,
                            user.LastLoginIp,
                            user.LastModifiedTime,
                            user.LastModifiedIp,
                            user.UserAuths,
                            user.Sort
                            );
                #endregion
                Bus.RaiseEvent(userscreatedevent);
            }

            return Task.FromResult(new Unit());
        }

        // 同上，UpdateUsersCommand 的处理方法
        public Task<Unit> Handle(UpdateUsersCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(new Unit());
            }
            var originuser = _UsersRepository.GetUserById(message.Id);

            var address = new Address(message.Province, message.City, message.County, message.Street, message.Detailed);
            var ip = string.Empty;
            SystemHelper.GetLocalIP(ref ip);
            var userAuths = message.UserAuths != null && message.UserAuths.Count > 0 ? message.UserAuths : originuser.UserAuths;
            #region 
            var user = new Users(
                    message.Id,
                    message.Type,
                    message.MtypeId,
                    message.NickName,
                    message.Surname,
                    message.Name,
                    message.RealName,
                    message.Phone,
                    message.Email,
                    message.BirthDate,
                    message.Sex,
                    message.Age,
                    message.Gravatar,
                    message.Avatar,
                    message.Motto,
                    message.Bio,
                    message.Idcard,
                    message.Major,
                    message.Polity,
                    message.NowState,
                    message.State,
                    address,
                    message.Company,
                    message.Website,
                    message.Weibo,
                    message.Blog,
                    message.Url,
                    message.RegisterTime,
                    message.RegisterIp,
                    message.LastLoginTime,
                    message.LastLoginIp,
                    DateTime.Now,//message.LastModifiedTime,
                    ip,//message.LastModifiedIp,
                    userAuths,
                    message.Sort
                    );
            #endregion

            // 判断用户名是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            var existingUsers = _UsersRepository.GetByName(user.Name);
            if (existingUsers != null && existingUsers.Id != user.Id)
            {
                //引发错误事件
                Bus.RaiseEvent(new DomainNotification("", "该用户名已经被使用！"));
                return Task.FromResult(new Unit());
            }
            // 判断邮箱是否存在
            existingUsers = _UsersRepository.GetByEmail(user.Email);
            if (existingUsers != null && existingUsers.Id != user.Id)
            {
                if (!existingUsers.Equals(user))
                {
                    Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                    return Task.FromResult(new Unit());
                }
            }

            _UsersRepository.Update(user);

            if (Commit())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                #region 
                var usersupdatedevent = new UsersUpdatedEvent(
                            user.Id,
                            user.Type,
                            user.MtypeId,
                            user.NickName,
                            user.Surname,
                            user.Name,
                            user.RealName,
                            user.Phone,
                            user.Email,
                            user.BirthDate,
                            user.Sex,
                            user.Age,
                            user.Gravatar,
                            user.Avatar,
                            user.Motto,
                            user.Bio,
                            user.Idcard,
                            user.Major,
                            user.Polity,
                            user.NowState,
                            user.State,
                            user.Address.Province, user.Address.City, user.Address.County, user.Address.Street, user.Address.Detailed,
                            user.Company,
                            user.Website,
                            user.Weibo,
                            user.Blog,
                            user.Url,
                            user.RegisterTime,
                            user.RegisterIp,
                            user.LastLoginTime,
                            user.LastLoginIp,
                            user.LastModifiedTime,
                            user.LastModifiedIp,
                            user.UserAuths,
                            user.Sort
                            );
                #endregion
                Bus.RaiseEvent(usersupdatedevent);
            }

            return Task.FromResult(new Unit());
        }

        public async Task<Response<Users>> Handle(ModifyUsersCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return new Response<Users>("Validation failed.");
            }
            var user = await _UsersRepository.FindByIdAsync(request.Id);

            if (user == null)
            {
                return new Response<Users>("User not found.");
            }

            #region 
            var address = new Address(request.Province, request.City, request.County, request.Street, request.Detailed);
            user.Type = request.Type;
            user.MtypeId = request.MtypeId;
            user.NickName = request.NickName;
            user.Surname = request.Surname;
            user.Name = request.Name;
            user.RealName = request.RealName;
            user.Phone = request.Phone;
            user.Email = request.Email;
            user.BirthDate = request.BirthDate;
            user.Sex = request.Sex;
            user.Age = request.Age;
            user.Gravatar = request.Gravatar;
            user.Avatar = request.Avatar;
            user.Motto = request.Motto;
            user.Bio = request.Bio;
            user.Idcard = request.Idcard;
            user.Major = request.Major;
            user.Polity = request.Polity;
            user.NowState = request.NowState;
            user.State = request.State;
            user.Address = address;
            user.Company = request.Company;
            user.Website = request.Website;
            user.Weibo = request.Weibo;
            user.Blog = request.Blog;
            user.Url = request.Url;
            user.RegisterTime = request.RegisterTime;
            user.RegisterIp = request.RegisterIp;
            user.LastLoginTime = request.LastLoginTime;
            user.LastLoginIp = request.LastLoginIp;
            user.LastModifiedTime = request.LastModifiedTime;
            user.LastModifiedIp = request.LastModifiedIp;
            user.UserAuths = request.UserAuths.Count > 0 ? request.UserAuths : user.UserAuths;
            user.Sort = request.Sort;
            //user.IsSuperMan = request.IsSuperMan; 
            #endregion

            _UsersRepository.Update(user);
            //var result = await CommitAsync();
            if (await CommitAsync())
            {
                #region 
                var usersmodifiedevent = new UsersModifiedEvent(
                            user.Id,
                            user.Type,
                            user.MtypeId,
                            user.NickName,
                            user.Surname,
                            user.Name,
                            user.RealName,
                            user.Phone,
                            user.Email,
                            user.BirthDate,
                            user.Sex,
                            user.Age,
                            user.Gravatar,
                            user.Avatar,
                            user.Motto,
                            user.Bio,
                            user.Idcard,
                            user.Major,
                            user.Polity,
                            user.NowState,
                            user.State,
                            user.Address.Province, user.Address.City, user.Address.County, user.Address.Street, user.Address.Detailed,
                            user.Company,
                            user.Website,
                            user.Weibo,
                            user.Blog,
                            user.Url,
                            user.RegisterTime,
                            user.RegisterIp,
                            user.LastLoginTime,
                            user.LastLoginIp,
                            user.LastModifiedTime,
                            user.LastModifiedIp,
                            user.UserAuths,
                            user.Sort
                            );
                #endregion
                await Bus.RaiseEvent(usersmodifiedevent);
            }

            return new Response<Users>(user);
        }

        // 同上，RemoveUsersCommand 的处理方法
        public Task<Unit> Handle(RemoveUsersCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(new Unit());

            }

            _UsersRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new UsersRemovedEvent(message.Id));
            }

            return Task.FromResult(new Unit());

        }

        // 手动回收
        public void Dispose()
        {
            _UsersRepository.Dispose();
        }
    }
}
