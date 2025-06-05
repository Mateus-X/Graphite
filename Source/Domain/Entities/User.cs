using Microsoft.AspNetCore.Identity;

namespace Graphite.Source.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();
    }
}