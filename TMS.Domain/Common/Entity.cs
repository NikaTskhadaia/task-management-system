using System;

namespace TMS.Domain.Common
{
    public abstract class Entity <T>
    {
        public T Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
