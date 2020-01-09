using System;

namespace GradeBook
{
  class Program
  {

    static void OnGradeAdded(object sender, EventArgs e)
    {
      System.Console.WriteLine("A grade was added");
    }

    static void Main(string[] args)
    {
      InMemoryBook book = new InMemoryBook("test");
      book.GradeAdded += OnGradeAdded;
      EnterGrades(book);

      Statistics stats = book.GetStatistics();
      Console.Write($"The lowest grade is: {stats.Low} \n");
      Console.Write($"The highest grade is: {stats.High} \n");
      Console.Write($"The average grade is: {stats.Average} \n");
      Console.Write($"The letter grade is: {stats.Letter} \n");
    }

    private static void EnterGrades(InMemoryBook book)
    {
      while (true)
      {
        Console.WriteLine("Geef een cijfer op (\"Q\" stopt de invoer):");
        string input = Console.ReadLine();

        if (input.ToLower() == "q")
        {
          break;
        }

        try
        {
          double grade = double.Parse(input);
          book.AddGrade(grade);
        }
        catch (ArgumentException ex)
        {
          Console.WriteLine(ex.Message);
        }
        catch (FormatException ex)
        {
          Console.WriteLine(ex.Message);
        }
        finally
        {
          //...
        }

      }
    }
  }
}
