using System;
using System.Collections.Generic;

namespace GradeBook
{

  public delegate void GradeAddedDelegate(object sender, EventArgs args);

  public class NamedObject {
    public NamedObject(string name)
    {
      Name = name;
    }

    public string Name {
      get;
      set;
    }
  }

  public abstract class Book: NamedObject, IBook
  {
    public Book(string name) : base(name)
    {
    }

    public abstract void AddGrade(double grade);

    public event GradeAddedDelegate GradeAdded;

    public virtual Statistics GetStatistics()
    {
      throw new NotImplementedException();
    }
  }

  public interface IBook
  {
    void AddGrade(double grade);
    Statistics GetStatistics();
    string Name { get; }
    event GradeAddedDelegate GradeAdded;
  }

  public class InMemoryBook: Book
  {

    private List<double> grades;
    public double High;
    public double Low;
    public double Average;


    private string name;

    public InMemoryBook(string name) : base(name)
    {
      Name = name;
      grades = new List<double>();
    }

    public override void AddGrade(double grade)
    {
      if (grade <= 100 && grade >= 0)
      {
        grades.Add(grade);
        if (GradeAdded != null) {
          GradeAdded(this, new EventArgs());
        }
      }
      else
      {
        throw new ArgumentException($"Invalid {nameof(grade)}");
      }
    }

    public void AddGrade(char letter)
    {
      switch (letter) {
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

    public double AverageGrade()
    {
      return Total() / grades.Count;
    }

    public double LowestGrade()
    {
      double _lowest = double.MaxValue;
      foreach(double grade in grades)
      {
        _lowest = System.Math.Min(grade, _lowest);
      }
      return _lowest;
    }

    public double HighestGrade()
    {
      double _highest = double.MinValue;
      foreach(double grade in grades)
      {
        _highest = System.Math.Max(grade, _highest);
      }
      return _highest;
    }

    public double Total()
    {
      double _total = 0;
      foreach(double grade in grades)
      {
        _total += grade;
      }
      return _total;
    }

    public override Statistics GetStatistics()
    {
      Statistics result = new Statistics();
      result.Average = AverageGrade();
      result.Low = LowestGrade();
      result.High = HighestGrade();

      switch (result.Average) {
        case var d when d >= 90.0:
          result.Letter = 'A';
          break;
        case var d when d >= 80.0:
          result.Letter = 'B';
          break;
        case var d when d >= 70.0:
          result.Letter = 'C';
          break;
        case var d when d >= 60.0:
          result.Letter = 'D';
          break;
        default:
          result.Letter = 'F';
          break;
      }
      
      return result;
    }

    public event GradeAddedDelegate GradeAdded;

  }

}