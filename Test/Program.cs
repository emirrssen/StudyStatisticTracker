using Entities.Concrete;
using System.Globalization;
using Test;


StudyPeriodManagerTest test = new StudyPeriodManagerTest();

var result = test.GetStudyPeriodsByWeekTest();
//Console.WriteLine($"Total number of studied days : {result.TotalNumberOfStudiedDays} days.\n" +
//    $"Total number of studied hours : {Math.Floor(result.TotalNumberOfStudiedHours.TotalHours)} hours and {result.TotalNumberOfStudiedHours.Minutes} minutes.\n" +
//    $"Average number of studied hours: {result.AverageNumberOfStudiedHours.Hours} hours and {result.AverageNumberOfStudiedHours.Minutes} minutes.");
foreach (var item in result)
{
    Console.WriteLine(item.Id + " | " + item.DateOfStudyPeriod + " | " + item.StartingTime + " | " + item.EndTime + " | " + item.Title + " | " + item.Annotation);
}


