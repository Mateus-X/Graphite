using Microsoft.AspNetCore.Identity;

namespace Graphite.Source.Domain.Entities
{
    public class User : IdentityUser
    {

        public virtual ICollection<Dataframe> Dataframes { get; set; } = new List<Dataframe>();
    }
}