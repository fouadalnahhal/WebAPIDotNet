using System.ComponentModel.DataAnnotations;

namespace WebAPIDotNet.Models
{
    public class ApiAction
    {
        [MaxLength(100)]
        public string Controller { get; set; }

        [MaxLength(100)]
        public string Action { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public virtual ICollection<ApiActionRole> ApiActionRoles { get; set; }
    }
}
