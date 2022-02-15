using Microsoft.AspNetCore.Identity;
using OnePlan.Business.Enums;
using OnePlan.Business.Models.Base;

namespace OnePlan.Business.Models;

public class User : IdentityUser<int>, IEntity<int>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public UserStatus Status { get; set; }
    
    public virtual List<UserRole> UserRoles { get; set; }
}
