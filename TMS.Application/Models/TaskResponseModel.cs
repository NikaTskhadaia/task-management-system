using System;

namespace TMS.Application.Models
{
    public class TaskResponseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
    }
}
