using Microsoft.AspNetCore.Identity;

namespace WebAPIDotNet.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName)
          : base(roleName)
        {
        }

        public ApplicationRole(Guid id, string roleName)
            : base(roleName)
        {
            Id = id;
        }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public virtual ICollection<ApiActionRole> ApiActionRoles { get; set; }
    }
}
