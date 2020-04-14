using AGooday.DncZero.Common.Helper;
using AGooday.DncZero.Domain.Commands.Users;
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
        IRequestHandler<RegisterUsersCommand, Unit>,
        IRequestHandler<UpdateUsersCommand, Unit>,
        IRequestHandler<RemoveUsersCommand, Unit>
    {
        // 注入仓储接口
        private readonly IUsersRepository _UsersRepository;
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
            IUnitOfWork uow,
            IMediatorHandler bus,
            IMemoryCache cache
            ) : base(uow, bus, cache)
        {
            _UsersRepository = usersRepository;
            Bus = bus;
            Cache = cache;
        }

        // RegisterUsersCommand命令的处理程序
        // 整个命令处理程序的核心都在这里
        // 不仅包括命令验证的收集，持久化，还有领域事件和通知的添加
        public Task<Unit> Handle(RegisterUsersCommand message, CancellationToken cancellationToken)
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
            var users = new Users(
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
                message.Sort
                );

            // 判断用户名是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            var existingUsers = _UsersRepository.GetByName(users.Name);
            if (existingUsers != null && existingUsers.Id != users.Id)
            {
                //引发错误事件
                Bus.RaiseEvent(new DomainNotification("", "该用户名已经被使用！"));
                return Task.FromResult(new Unit());
            }
            // 判断邮箱是否存在
            existingUsers = _UsersRepository.GetByEmail(users.Email);
            if (existingUsers != null && existingUsers.Id != users.Id)
            {
                if (!existingUsers.Equals(users))
                {
                    Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                    return Task.FromResult(new Unit());
                }
            }

            // 持久化
            _UsersRepository.Add(users);

            // 统一提交
            if (Commit())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                var usersregisteredevent = new UsersRegisteredEvent(
                    users.Id,
                    users.Type,
                    users.MtypeId,
                    users.NickName,
                    users.Surname,
                    users.Name,
                    users.RealName,
                    users.Phone,
                    users.Email,
                    users.BirthDate,
                    users.Sex,
                    users.Age,
                    users.Gravatar,
                    users.Avatar,
                    users.Motto,
                    users.Bio,
                    users.Idcard,
                    users.Major,
                    users.Polity,
                    users.NowState,
                    users.State,
                    users.Address.Province, users.Address.City, users.Address.County, users.Address.Street, users.Address.Detailed,
                    users.Company,
                    users.Website,
                    users.Weibo,
                    users.Blog,
                    users.Url,
                    users.RegisterTime,
                    users.RegisterIp,
                    users.LastLoginTime,
                    users.LastLoginIp,
                    users.LastModifiedTime,
                    users.LastModifiedIp,
                    users.Sort
                    );
                Bus.RaiseEvent(usersregisteredevent);
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

            var address = new Address(message.Province, message.City, message.County, message.Street, message.Detailed);
            var ip = string.Empty;
            SystemHelper.GetLocalIP(ref ip);
            var users = new Users(
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
                message.Sort
                );

            // 判断用户名是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            var existingUsers = _UsersRepository.GetByName(users.Name);
            if (existingUsers != null && existingUsers.Id != users.Id)
            {
                //引发错误事件
                Bus.RaiseEvent(new DomainNotification("", "该用户名已经被使用！"));
                return Task.FromResult(new Unit());
            }
            // 判断邮箱是否存在
            existingUsers = _UsersRepository.GetByEmail(users.Email);
            if (existingUsers != null && existingUsers.Id != users.Id)
            {
                if (!existingUsers.Equals(users))
                {
                    Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                    return Task.FromResult(new Unit());
                }
            }

            _UsersRepository.Update(users);

            if (Commit())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                var usersupdatedevent = new UsersUpdatedEvent(
                    users.Id,
                    users.Type,
                    users.MtypeId,
                    users.NickName,
                    users.Surname,
                    users.Name,
                    users.RealName,
                    users.Phone,
                    users.Email,
                    users.BirthDate,
                    users.Sex,
                    users.Age,
                    users.Gravatar,
                    users.Avatar,
                    users.Motto,
                    users.Bio,
                    users.Idcard,
                    users.Major,
                    users.Polity,
                    users.NowState,
                    users.State,
                    users.Address.Province, users.Address.City, users.Address.County, users.Address.Street, users.Address.Detailed,
                    users.Company,
                    users.Website,
                    users.Weibo,
                    users.Blog,
                    users.Url,
                    users.RegisterTime,
                    users.RegisterIp,
                    users.LastLoginTime,
                    users.LastLoginIp,
                    users.LastModifiedTime,
                    users.LastModifiedIp,
                    users.Sort
                    );
                Bus.RaiseEvent(usersupdatedevent);
            }

            return Task.FromResult(new Unit());
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
