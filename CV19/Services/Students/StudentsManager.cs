using System.Collections.Generic;
using CV19.Models.Decanat;

namespace CV19.Services.Students
{
  public class StudentsManager
  {
    private readonly StudentsRepository _students;
    private readonly GroupsPepository _groups;

    public IEnumerable<Student> Students => _students.GetAll();
    public IEnumerable<Group> Groups => _groups.GetAll();

    public StudentsManager(StudentsRepository students, GroupsPepository groups)
    {
      _students = students;
      _groups = groups;
    }
  }
}