using AGooday.DncZero.Application.EventSourcing;
using AGooday.DncZero.Application.Interfaces;
using AGooday.DncZero.Application.Services;
using AGooday.DncZero.Domain.CommandHandlers;
using AGooday.DncZero.Domain.Commands.Users;
using AGooday.DncZero.Domain.Core.Bus;
using AGooday.DncZero.Domain.Core.Events;
using AGooday.DncZero.Domain.Core.Notifications;
using AGooday.DncZero.Domain.EventHandlers;
using AGooday.DncZero.Domain.Events.Users;
using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Infrastructure.Bus;
using AGooday.DncZero.Infrastructure.Context;
using AGooday.DncZero.Infrastructure.Identity.Authorization;
using AGooday.DncZero.Infrastructure.Identity.Models;
using AGooday.DncZero.Infrastructure.Identity.Services;
using AGooday.DncZero.Infrastructure.Repository;
using AGooday.DncZero.Infrastructure.Repository.EventSourcing;
using AGooday.DncZero.Infrastructure.UoW;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGooday.DncZero.Web.Extensions
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // 注入 应用层Application
            services.AddScoped<IUsersAppService, UsersAppService>();

            // 命令总线Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain - Events
            // 将事件模型和事件处理程序匹配注入

            // 领域通知
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            // 领域事件
            services.AddScoped<INotificationHandler<UsersRegisteredEvent>, UsersEventHandler>();
            services.AddScoped<INotificationHandler<UsersUpdatedEvent>, UsersEventHandler>();
            services.AddScoped<INotificationHandler<UsersRemovedEvent>, UsersEventHandler>();

            // 领域层 - 领域命令
            // 将命令模型和命令处理程序匹配
            services.AddScoped<IRequestHandler<RegisterUsersCommand, Unit>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUsersCommand, Unit>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveUsersCommand, Unit>, UsersCommandHandler>();

            // 领域层 - Memory缓存
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            // 注入 基础设施层 - 数据层
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<DncZeroDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 注入 基础设施层 - 事件溯源
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStoreService, SqlEventStoreService>();
            services.AddScoped<EventStoreSQLContext>();

            // 注入 基础设施层 - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // 注入 基础设施层 - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
