using AGooday.DncZero.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Events.Users
{
    public class UsersRegisteredEvent : Event
    {
        public UsersRegisteredEvent(Guid id, string name, string email, DateTime? birthDate, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Phone = phone;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime? BirthDate { get; private set; }

        public string Phone { get; private set; }
    }
}
