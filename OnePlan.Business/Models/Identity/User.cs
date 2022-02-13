using Microsoft.AspNetCore.Identity;
using OnePlan.Business.Models.Base;

namespace OnePlan.Business.Models.Identity;

public class User : IdentityUser<int>, IEntity<int>
{
    public bool isHide { get; set; }
    
    public virtual List<UserRole> UserRoles { get; set; }
}
