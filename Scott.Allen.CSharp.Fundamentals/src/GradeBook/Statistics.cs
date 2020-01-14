using System;

namespace GradeBook
{
  public class Statistics
  {
    public double Low;
    public double High;
    public double Average
    {
      get
      {
        return Sum / Count;
      }
    }
    public char Letter
    {
      get
      {
        switch (Average)
        {
          case var d when d >= 90.0:
            return 'A';
          case var d when d >= 80.0:
            return 'B';
          case var d when d >= 70.0:
            return 'C';
          case var d when d >= 60.0:
            return 'D';
          default:
            return 'F';
        }
      }
    }
    public double Sum;
    public int Count;

    public Statistics()
    {
      Sum = 0.0;
      Low = Double.MaxValue;
      High = Double.MinValue;
      Count = 0;
    }

    public void Add(double number)
    {
      Low = Math.Min(number, Low);
      High = Math.Max(number, High);
      Sum += number;
      Count++;
    }

  }
}