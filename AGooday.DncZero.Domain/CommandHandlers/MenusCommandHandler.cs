using AGooday.DncZero.Common.Extensions;
using AGooday.DncZero.Common.Helper;
using AGooday.DncZero.Domain.Commands.Menus;
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
    /// Menus 领域命令处理程序
    /// 
    /// IRequestHandler 也是通过结构类型Unit来处理不需要返回值的情况。
    /// </summary>
    public class MenusCommandHandler : CommandHandler,
        IRequestHandler<CreateMenusCommand, Unit>
    {
        // 注入仓储接口
        private readonly IMenusRepository _MenusRepository;
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
        public MenusCommandHandler(IMenusRepository menusRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            IMemoryCache cache
            ) : base(uow, bus, cache)
        {
            _MenusRepository = menusRepository;
            Bus = bus;
            Cache = cache;
        }

        // RegisterUsersCommand命令的处理程序
        // 整个命令处理程序的核心都在这里
        // 不仅包括命令验证的收集，持久化，还有领域事件和通知的添加
        public async Task<Unit> Handle(CreateMenusCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return await Task.FromResult(new Unit());
            }
            #region 
            var menu = new Menus();
            menu.Name = request.Name;
            //user.IsSuperMan = request.IsSuperMan; 
            #endregion

            #region 检查
            // 判断名称是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            var existingUsers = _MenusRepository.GetByName(menu.Name);
            if (existingUsers != null && existingUsers.Id != menu.Id)
            {
                //引发错误事件
                await Bus.RaiseEvent(new DomainNotification("", "该名称已经被使用！"));
                return await Task.FromResult(new Unit());
            }
            #endregion

            await _MenusRepository.AddAsync(menu);
            var result = await CommitAsync();

            return await Task.FromResult(new Unit());
        }

        // 手动回收
        public void Dispose()
        {
            _MenusRepository.Dispose();
        }
    }
}
