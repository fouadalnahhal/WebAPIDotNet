using Microsoft.AspNetCore.Identity;

namespace WebAPIDotNet.Models
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationRole ApplicationRoleEntity { get; set; }
    }
}
