using AGooday.DncZero.Domain.Events.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AGooday.DncZero.Domain.EventHandlers
{
    public class UsersEventHandler :
        INotificationHandler<UsersRegisteredEvent>,
        INotificationHandler<UsersCreatedEvent>,
        INotificationHandler<UsersModifiedEvent>,
        INotificationHandler<UsersUpdatedEvent>,
        INotificationHandler<UsersRemovedEvent>
    {
        // Users 被注册成功后的事件处理方法
        public Task Handle(UsersRegisteredEvent message, CancellationToken cancellationToken)
        {
            // 恭喜您，注册成功，欢迎加入我们。

            return Task.CompletedTask;
        }

        public Task Handle(UsersCreatedEvent message, CancellationToken cancellationToken)
        {
            // 恭喜您，注册成功，欢迎加入我们。

            return Task.CompletedTask;
        }

        // Users 被修改成功后的事件处理方法
        public Task Handle(UsersUpdatedEvent message, CancellationToken cancellationToken)
        {
            // 恭喜您，更新成功，请牢记修改后的信息。

            return Task.CompletedTask;
        }

        public Task Handle(UsersModifiedEvent message, CancellationToken cancellationToken)
        {
            // 恭喜您，更新成功，请牢记修改后的信息。

            return Task.CompletedTask;
        }

        // Users 被删除后的事件处理方法
        public Task Handle(UsersRemovedEvent message, CancellationToken cancellationToken)
        {
            // 您已经删除成功啦，记得以后常来看看。

            return Task.CompletedTask;
        }
    }
}
