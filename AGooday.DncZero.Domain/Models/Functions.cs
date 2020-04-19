using AGooday.DncZero.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Models
{
    public class Functions : SortableEntity<Guid, long>
    {
        public Guid MenuId { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Area { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public string Url { get; private set; }
    }
}
