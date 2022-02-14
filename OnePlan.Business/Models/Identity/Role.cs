using Microsoft.AspNetCore.Identity;

namespace OnePlan.Business.Models;

public class Role : IdentityRole<int>
{
    public virtual List<UserRole> UserRoles { get; set; }
}
