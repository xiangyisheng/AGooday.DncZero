using AGooday.DncZero.Application.EventSourcing;
using AGooday.DncZero.Application.Interfaces;
using AGooday.DncZero.Application.Services;
using AGooday.DncZero.Domain.CommandHandlers;
using AGooday.DncZero.Domain.Commands.Users;
using AGooday.DncZero.Domain.Communication;
using AGooday.DncZero.Domain.Core.Bus;
using AGooday.DncZero.Domain.Core.Events;
using AGooday.DncZero.Domain.Core.Notifications;
using AGooday.DncZero.Domain.EventHandlers;
using AGooday.DncZero.Domain.Events.Users;
using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Domain.Models;
using AGooday.DncZero.Domain.Queries;
using AGooday.DncZero.Domain.Queries.Users;
using AGooday.DncZero.Domain.QueryHandler;
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
        /// <summary>
        /// services.AddTransient<IApplicationService,ApplicationService>//服务在每次请求时被创建，它最好被用于轻量级无状态服务（如我们的Repository和ApplicationService服务）
        /// services.AddScoped<IApplicationService, ApplicationService>//服务在每次请求时被创建，生命周期横贯整次请求
        /// services.AddSingleton<IApplicationService, ApplicationService>//Singleton（单例） 服务在第一次请求时被创建（或者当我们在ConfigureServices中指定创建某一实例并运行方法），其后的每次请求将沿用已创建服务。如果开发者的应用需要单例服务情景，请设计成允许服务容器来对服务生命周期进行操作，而不是手动实现单例设计模式然后由开发者在自定义类中进行操作。
        /// 
        /// 权重：AddSingleton→AddTransient→AddScoped
        /// AddSingleton的生命周期：项目启动-项目关闭 相当于静态类  只会有一个
        /// AddScoped的生命周期：请求开始-请求结束 在这次请求中获取的对象都是同一个
        /// AddTransient的生命周期：请求获取-（GC回收-主动释放） 每一次获取的对象都不是同一个
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency => ASP.NET HttpContext依赖项
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // ASP.NET Authorization Polices => ASP.NET授权策略
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // 注入 应用层Application
            services.AddScoped<IUsersAppService, UsersAppService>();
            services.AddScoped<IAuthorityAppService, AuthorityAppService>();

            // 命令总线Domain Bus (Mediator) 中介总线接口
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
            services.AddScoped<IRequestHandler<CreateUsersCommand, Unit>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterUsersCommand, Response<Users>>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUsersCommand, Unit>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<ModifyUsersCommand, Response<Users>>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveUsersCommand, Unit>, UsersCommandHandler>();

            services.AddScoped<IRequestHandler<GetByIdQuery<Users>, Users>, UsersQueryHandler>();
            services.AddScoped<IRequestHandler<ListUsersQuery, IEnumerable<Users>>, UsersQueryHandler>();

            // 领域层 - Memory缓存
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            // 注入 基础设施层 - 数据层
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserAuthsRepository, UserAuthsRepository>();
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
