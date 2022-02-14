using Microsoft.AspNetCore.Identity;
namespace OnePlan.Business.Models;

public class UserRole : IdentityUserRole<int>
{
    public virtual User User { get; set; }
    public virtual Role role { get; set; }
}
