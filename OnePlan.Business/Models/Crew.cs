using OnePlan.Business.Models.Base;

namespace OnePlan.Business.Models;

public class Crew : BaseEntity<int>
{
    public virtual  List<People> Peoples { get; set; }
}
