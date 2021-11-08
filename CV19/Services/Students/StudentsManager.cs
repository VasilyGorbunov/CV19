using System;
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

    public void Update(Student student) => _students.Update(student.Id, student);


    public bool Create(Student student, string groupName)
    {
      if(student is null) throw new ArgumentNullException(nameof(student));
      if(string.IsNullOrWhiteSpace(groupName)) throw new ArgumentException("Некорректное имя группы", nameof(groupName));

      var group = _groups.Get(groupName);

      if (group is null)
      {
        group = new Group{ Name = groupName };
        _groups.Add(group);
      }
      group.Students.Add(student);
      _students.Add(student);

      return true;
    }
  }
}