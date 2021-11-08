using System.Collections.Generic;
using CV19.Models.Interfaces;

namespace CV19.Models.Decanat
{
  public class Group : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<Student> Students { get; set; } = new List<Student>();
    public string Description { get; set; }
    
  }
}