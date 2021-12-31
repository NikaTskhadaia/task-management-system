using System;
using TMS.Domain.Common;
using TMS.Domain.ValueObjects;

namespace TMS.Domain.Entities
{
    public class TaskEntity : Entity<Guid>
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public TaskFiles Files { get; set; }
        public string UserName { get; set; }
    }
}
