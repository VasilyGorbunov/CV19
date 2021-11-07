using System;
using System.Collections.Generic;
using System.Linq;
using CV19.Models.Decanat;

namespace CV19.Services.Students
{
  public static class TestData
  {
    public static Group[] Groups { get; } = Enumerable.Range(1, 10)
      .Select(i => new Group { Name = $"Группа {i}"})
      .ToArray();

    public static Student[] Students { get; } = CreateStudents(Groups);

    private static Student[] CreateStudents(Group[] groups)
    {
      var rnd = new Random();
      var index = 1;
      var students = new List<Student>(100);
      foreach (var group in groups)
      {
        for (int i = 0; i < 10; i++)
        {
          group.Students.Add(new Student
          {
            Name = $"Имя {index}",
            Surname = $"Фамилия {index}",
            Patronymic = $"Отчество {index++}",
            Birthday = DateTime.Now.Subtract(TimeSpan.FromDays(300 * rnd.Next(19, 30))),
            Rating = rnd.NextDouble() * 100
          });
        }
      }

      return groups.SelectMany(g => g.Students).ToArray();
    }
  }
}