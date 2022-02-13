using System.Collections.Generic;

namespace OnePlan.Business.Models.Base;
/// <summary>
/// Base entity for all entries
/// </summary>
/// <typeparam name="T">Type of primary key</typeparam>
public class BaseEntity<T> : IEntity<T>
{
    public T Id { get; set; }
    
    public DateTime CreatedDate { get; set; }
    public DateTime Updateddate { get; set; }
}
