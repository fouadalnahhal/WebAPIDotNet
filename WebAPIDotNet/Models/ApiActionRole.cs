namespace WebAPIDotNet.Models
{
    public class ApiActionRole
    {
        public Guid ActionId { get; set; }

        public virtual ApiAction ApiAction { get; set; } = null!;

        public Guid RoleId { get; set; }

        public virtual ApplicationRole Role { get; set; } = null!;
    }
}
