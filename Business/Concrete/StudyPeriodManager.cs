using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StudyPeriodManager : IStudyPeriodService
    {
        IStudyPeriodDal _studyPeriodDal;

        public StudyPeriodManager(IStudyPeriodDal studyPeriodDal)
        {
            _studyPeriodDal = studyPeriodDal;
        }

        // Search Autofac and implement into program.
        // Implement FluentValidation into program.
        // Write StudyPeriodValidator validation class.
        // [ValidationAspect("StudyPeriodValidator")]
        public IResult AddStudyPeriod(StudyPeriod studyPeriod)
        {
            _studyPeriodDal.Add(studyPeriod);
            return new SuccessResult();
        }

        public IResult DeleteStudyPeriod(StudyPeriod studyPeriod)
        {
            _studyPeriodDal.Delete(studyPeriod);
            return new SuccessResult();
        }

        public IDataResult<List<StudyPeriod>> GetAllStudyPeriods()
        {
            return new SuccessDataResult<List<StudyPeriod>>(_studyPeriodDal.GetAll());
        }

        public IDataResult<List<StudyPeriod>> GetStudyPeriodsByDate(DateTime date)
        {
            return new SuccessDataResult<List<StudyPeriod>>(_studyPeriodDal.GetAll(x => x.DateOfStudyPeriod == date));
        }

        public IDataResult<List<StudyPeriod>> GetStudyPeriodsByDateRange(DateTime startingDate, DateTime endingDate)
        {
            List<StudyPeriod> studyPeriods = new List<StudyPeriod>();
            List<DateTime> dates = GetDatesBetween(startingDate, endingDate);

            for (int i = 0; i < dates.Count; i++)
            {
                var result = GetStudyPeriodsByDate(dates[i]);

                foreach (var studyPeriod in result.Data)
                {
                    studyPeriods.Add(studyPeriod);
                }
            }

            return new SuccessDataResult<List<StudyPeriod>>(studyPeriods);
        }

        public IDataResult<List<StudyPeriod>> GetStudyPeriodsByMonth(int month)
        {
            return new SuccessDataResult<List<StudyPeriod>>(_studyPeriodDal.GetAll(x => x.DateOfStudyPeriod.Month == month));
        }

        public IDataResult<List<StudyPeriod>> GetStudyPeriodsByWeek()
        {
            List<StudyPeriod> studyPeriods = new List<StudyPeriod>();
            int weekNumberOfToday = GetWeekNumber(DateTime.Today);

            foreach (var item in _studyPeriodDal.GetAll())
            {
                if (GetWeekNumber(item.DateOfStudyPeriod) == weekNumberOfToday)
                {
                    studyPeriods.Add(item);
                }
            }
            return new SuccessDataResult<List<StudyPeriod>>(studyPeriods);
        }

        public IDataResult<List<StudyPeriod>> GetStudyPeriodsByYear(int year)
        {
            return new SuccessDataResult<List<StudyPeriod>>(_studyPeriodDal.GetAll(x => x.DateOfStudyPeriod.Year == year));
        }

        public IResult UpdateStudyPeriod(StudyPeriod studyPeriod)
        {
            _studyPeriodDal.Update(studyPeriod);
            return new SuccessResult();
        }

        public IDataResult<StudyReportDto> CreateStudyReportByDateRange(DateTime startingDate, DateTime endingDate)
        {
            List<StudyPeriod> studyPeriods = GetStudyPeriodsByDateRange(startingDate, endingDate).Data;
            double totalStudiedMinutes = CalculateTotalStudiedMinutes(studyPeriods);
            int totalStudiedDays = GetDatesBetween(startingDate, endingDate).Count;
            double averageStudiedMinutes = totalStudiedMinutes / totalStudiedDays;

            return new SuccessDataResult<StudyReportDto>(new StudyReportDto
            {
                StudyPeriods = GetStudyPeriodsByDateRange(startingDate, endingDate).Data,
                TotalNumberOfStudiedHours = TimeSpan.FromMinutes(totalStudiedMinutes),
                TotalNumberOfStudiedDays = totalStudiedDays,
                AverageNumberOfStudiedHours = TimeSpan.FromMinutes(averageStudiedMinutes)
            });
        }

        public IDataResult<StudyReportDto> CreateStudyReportByDay(DateTime date)
        {
            List<StudyPeriod> studyPeriods = new List<StudyPeriod>();
            var result = GetStudyPeriodsByDate(date);
            double totalStudiedMinutes = 0;

            foreach (var studyPeriod in result.Data)
            {
                studyPeriods.Add(studyPeriod);
                totalStudiedMinutes += (studyPeriod.EndTime - studyPeriod.StartingTime).TotalMinutes;
            }

            return new SuccessDataResult<StudyReportDto>(new StudyReportDto { StudyPeriods = studyPeriods, TotalNumberOfStudiedHours = TimeSpan.FromMinutes(totalStudiedMinutes) });
        }

        public IDataResult<StudyPredispositionDto> CalculateStudyPredisposition()
        {
            throw new NotImplementedException();
        }

        private int GetWeekNumber(DateTime date)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        private List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            return allDates;
        }

        private double CalculateTotalStudiedMinutes(List<StudyPeriod> studyPeriods)
        {
            double totalStudiedMinutes = 0;
            foreach (var studyPeriod in studyPeriods)
            {
                totalStudiedMinutes += (studyPeriod.EndTime - studyPeriod.StartingTime).TotalMinutes;
            }

            return totalStudiedMinutes;
        }

        private DateTime FindFirstStudyPeriodRecordDate()
        {
            List<DateTime> dates = new List<DateTime>();
            var result = GetAllStudyPeriods().Data;

            foreach (var item in result)
            {
                dates.Add(item.DateOfStudyPeriod);
            }

            return dates.Min();
        }

        private List<string> ConvertDatesIntoIso(List<DateTime> dates)
        {
            List<string> convertedDates = new List<string>();

            foreach (var date in dates)
            {
                convertedDates.Add(date.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            }

            return convertedDates;
        }
    }
}
