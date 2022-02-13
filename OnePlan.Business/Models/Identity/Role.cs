using Microsoft.AspNetCore.Identity;

namespace OnePlan.Business.Models.Identity;

public class Role : IdentityRole<int>
{
    public virtual List<UserRole> UserRoles { get; set; }
}
