using System.ComponentModel.DataAnnotations;

namespace TMS.Application.Models
{
    public class TaskModel
    {
        [Required]
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
