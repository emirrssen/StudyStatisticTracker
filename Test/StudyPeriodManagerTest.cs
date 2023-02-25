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

        public void GetAllStudyPeriodsTest()
        {
            var result = studyPeriodManager.GetAllStudyPeriods();
            foreach (var item in result.Data)
            {
                Console.WriteLine(item.Id + " | " + item.DateOfStudyPeriod + " | " + item.StartingTime + " | " + item.EndTime + " | " + item.Title + " | " + item.Annotation);
            }
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
            return studyPeriodManager.CreateStudyReportByDay(DateTime.Today).Data;
        }
    }
}
