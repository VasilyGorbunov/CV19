using CV19.Models.Decanat;
using CV19.Services.Base;

namespace CV19.Services.Students
{
  public class GroupsPepository: RepositoryInMemory<Group>
  {
    public GroupsPepository() : base(TestData.Groups) { }

    protected override void Update(Group source, Group destination)
    {
      destination.Name = source.Name;
      destination.Description = source.Description;
    }
  }
}