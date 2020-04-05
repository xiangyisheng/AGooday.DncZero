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

            // 实例化领域模型，这里才真正的用到了领域模型
            // 注意这里是通过构造函数方法实现
            var address = new Address(message.Province, message.City, message.County, message.Street);
            var users = new Users(Guid.NewGuid(), message.NickName, message.Surname, message.Name, message.RealName, message.Email, message.Phone, message.BirthDate, address);

            // 判断邮箱是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            if (_UsersRepository.GetByEmail(users.Email) != null)
            {
                //引发错误事件
                Bus.RaiseEvent(new DomainNotification("", "该Name已经被使用！"));
                return Task.FromResult(new Unit());
            }

            // 持久化
            _UsersRepository.Add(users);

            // 统一提交
            if (Commit())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                Bus.RaiseEvent(new UsersRegisteredEvent(users.Id, users.Name, users.Email, users.BirthDate, users.Phone));
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


            var address = new Address(message.Province, message.City, message.County, message.Street);
            var users = new Users(message.Id, message.NickName, message.Surname, message.Name, message.RealName, message.Email, message.Phone, message.BirthDate, address);
            var existingStudent = _UsersRepository.GetByEmail(users.Email);

            if (existingStudent != null && existingStudent.Id != users.Id)
            {
                if (!existingStudent.Equals(users))
                {

                    Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                    return Task.FromResult(new Unit());

                }
            }

            _UsersRepository.Update(users);

            if (Commit())
            {

                Bus.RaiseEvent(new UsersUpdatedEvent(users.Id, users.Name, users.Email, users.BirthDate, users.Phone));
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
