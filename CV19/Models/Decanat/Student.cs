using System;

namespace CV19.Models.Decanat
{
  public class Student
  {
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public double Rating { get; set; }
  }
}