using OnePlan.Business.Models.Base;

namespace OnePlan.Business.Models;

public class People : BaseEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public virtual List<Crew> Crew { get; set; }
}
