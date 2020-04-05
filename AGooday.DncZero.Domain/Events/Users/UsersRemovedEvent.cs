using AGooday.DncZero.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Events.Users
{
    public class UsersRemovedEvent : Event
    {
        public UsersRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
