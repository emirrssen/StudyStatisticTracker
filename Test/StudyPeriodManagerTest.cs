using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class StudyPeriodManagerTest
    {
        StudyPeriodManager studyPeriodManager = new StudyPeriodManager(new EfStudyPeriodDal());
        EfStudyPeriodDal database = new EfStudyPeriodDal();

        public void AddStudyPeriodTest()
        {
            studyPeriodManager.AddStudyPeriod(new StudyPeriod
            {
                DateOfStudyPeriod = DateTime.Today,
                StartingTime = new TimeSpan(20, 0, 0),
                EndTime = new TimeSpan(22, 0, 0),
                Title = "Financial Stability",
            });
            GetAllStudyPeriodsTest();
        }

        public List<StudyPeriod> GetAllStudyPeriodsTest()
        {
            var result = studyPeriodManager.GetAllStudyPeriods();
            //foreach (var item in result.Data)
            //{
            //    Console.WriteLine(item.Id + " | " + item.DateOfStudyPeriod + " | " + item.StartingTime + " | " + item.EndTime + " | " + item.Title + " | " + item.Annotation);
            //}
            return result.Data;
        }

        public void DeleteStudyPeriodTest()
        {
            var result = studyPeriodManager.GetStudyPeriodsByDate(new DateTime(2023, 2, 20));
            studyPeriodManager.DeleteStudyPeriod(result.Data.Last());
            GetAllStudyPeriodsTest();
        }

        public void UpdateStudyPeriodTest()
        {
            var studyPeriodToUpdate = studyPeriodManager.GetAllStudyPeriods().Data.Last();
            studyPeriodToUpdate.Annotation = "Worked on StudyStatisticTracker project!";
            studyPeriodManager.UpdateStudyPeriod(studyPeriodToUpdate);
            GetAllStudyPeriodsTest();
        }

        public StudyReportDto CreateStudyReportByDateRangeTest()
        {
            return studyPeriodManager.CreateStudyReportByDateRange(new DateTime(2023, 1, 1), new DateTime(2023, 2, 20)).Data;
        }

        public List<StudyPeriod> GetStudyPeriodsByDateRangeTest()
        {
            return studyPeriodManager.GetStudyPeriodsByDateRange(new DateTime(2023, 1, 29), new DateTime(2023, 2, 20)).Data;
        }

        public List<StudyPeriod> GetStudyPeriodsByMonthTest()
        {
            return studyPeriodManager.GetStudyPeriodsByMonth(1).Data;
        }

        public List<StudyPeriod> GetStudyPeriodsByWeekTest()
        {
            return studyPeriodManager.GetStudyPeriodsByWeek().Data;
        }

        public List<StudyPeriod> GetStudyPeriodsByYearTest()
        {
            return studyPeriodManager.GetStudyPeriodsByYear(2023).Data;
        }

        public StudyReportDto CreateStudyReportByDayTest()
        {
            return studyPeriodManager.CreateStudyReportByDate(DateTime.Today).Data;
        }

        public void CalculateMostRepetitiveTimes()
        {
            var result = database.GetAll();
            int[] startingTimes = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
            int[] endingTimes = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (var item in result)
            {
                for (int i = 6; i < 24; i++)
                {
                    if (item.StartingTime.Hours == i)
                    {
                        startingTimes[i - 6]++;
                    }

                    if (item.EndTime.Hours == i)
                    {
                        endingTimes[i - 6]++;
                    }
                }
            }

            Console.WriteLine("For Starting Times");
            Console.WriteLine("-------------------------");
            for (int i = 0; i < startingTimes.Length; i++)
            {
                Console.WriteLine($"0{i+6}:00 -> {startingTimes[i]}");
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine("For Ending Times");
            Console.WriteLine("-------------------------");
            for (int i = 0; i < startingTimes.Length; i++)
            {
                Console.WriteLine($"0{i+6}:00 -> {endingTimes[i]}");
            }
        }

        public void CalculateWhichStartsWhichEndsMost(int startTime)
        {
            var result = database.GetAll(x => x.StartingTime.Hours == startTime).Select(x => x.EndTime);
            foreach (var item in result)
            {
                Console.WriteLine(item + "\n");
            }
        }
    }
}
