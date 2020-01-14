using System;
using System.IO;
using System.Collections.Generic;

namespace GradeBook
{

  public delegate void GradeAddedDelegate(object sender, EventArgs args);

  public class NamedObject
  {
    public NamedObject(string name)
    {
      Name = name;
    }
    public string Name
    {
      get;
      set;
    }
  }

  public abstract class Book : NamedObject, IBook
  {
    public Book(string name) : base(name)
    {
    }

    public abstract void AddGrade(double grade);

    public abstract event GradeAddedDelegate GradeAdded;

    public abstract Statistics GetStatistics();
  }

  public interface IBook
  {
    void AddGrade(double grade);
    Statistics GetStatistics();
    string Name { get; }
    event GradeAddedDelegate GradeAdded;
  }

  public class DiskBook : IBook
  {

    private List<double> Grades;
    public string Name { get; }

    public DiskBook(string name)
    {
      Grades = new List<double>();
    }

    public void AddGrade(double Grade)
    {
      using (System.IO.StreamWriter writer = File.AppendText($"{Name}.txt"))
      {
        writer.WriteLine(Grade);
        if (GradeAdded != null)
        {
          GradeAdded(this, new EventArgs());
        }
      }
    }
    public Statistics GetStatistics()
    {
      Statistics result = new Statistics();
      using (var reader = File.OpenText($"{Name}.txt"))
      {
        var line = reader.ReadLine();
        while (line != null)
        {
          double d = double.Parse(line);
          result.Add(d);
          line = reader.ReadLine();
        }
      }
      return result;
    }
    public event GradeAddedDelegate GradeAdded;
  }

  public class InMemoryBook : Book
  {

    private List<double> Grades;

    public InMemoryBook(string name) : base(name)
    {
      Name = name;
      Grades = new List<double>();
    }

    public override void AddGrade(double Grade)
    {
      if (Grade <= 100 && Grade >= 0)
      {
        Grades.Add(Grade);
        if (GradeAdded != null)
        {
          GradeAdded(this, new EventArgs());
        }
      }
      else
      {
        throw new ArgumentException($"Invalid {nameof(Grade)}");
      }
    }

    public void AddGrade(char letter)
    {
      switch (letter)
      {
        case 'A':
          AddGrade(90);
          break;
        case 'B':
          AddGrade(80);
          break;
        case 'C':
          AddGrade(70);
          break;
        case 'D':
          AddGrade(60);
          break;
        default:
          AddGrade(0);
          break;
      }
    }

    public override Statistics GetStatistics()
    {
      Statistics result = new Statistics();
      foreach (double Grade in Grades)
      {
        result.Add(Grade);
      }
      return result;
    }

    public override event GradeAddedDelegate GradeAdded;
  }

}