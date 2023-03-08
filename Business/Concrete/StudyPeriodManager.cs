using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Utilities.Business;
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
        //This is going to be use on constructor injection.
        IStudyPeriodDal _studyPeriodDal;

        public StudyPeriodManager(IStudyPeriodDal studyPeriodDal)
        {
            _studyPeriodDal = studyPeriodDal;
        }

        [ValidationAspect(typeof(StudyPeriodValidator))]
        [SecuredOperation("user")]
        [CacheRemoveAspect("IStudyPeriodService.Get")]
        public IResult AddStudyPeriod(StudyPeriod studyPeriod)
        {
            //IResult aspectResults = AspectResults.Check(Results.ValidationResult, Results.SecuredOperationResult);
            //if (aspectResults != null)
            //{
            //    return aspectResults;
            //}
            _studyPeriodDal.Add(studyPeriod);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IStudyPeriodService.Get")]
        [SecuredOperation("user")]
        public IResult DeleteStudyPeriod(StudyPeriod studyPeriod)
        {
            IResult aspectResults = AspectResults.Check(Results.SecuredOperationResult);
            if (aspectResults != null)
            {
                return aspectResults;
            }
            _studyPeriodDal.Delete(studyPeriod);
            return new SuccessResult();
        }

        [SecuredOperation("user")]
        public IDataResult<List<StudyPeriod>> GetAllStudyPeriods()
        {
            IResult aspectResults = AspectResults.Check(Results.SecuredOperationResult);

            if (aspectResults != null)
            {
                return new ErrorDataResult<List<StudyPeriod>>(null, aspectResults.Message);
            }

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

        [CacheRemoveAspect("IStudyPeriodService.Get")]
        [ValidationAspect(typeof(StudyPeriodValidator))]
        [SecuredOperation("user")]
        public IResult UpdateStudyPeriod(StudyPeriod studyPeriod)
        {
            IResult aspectResults = AspectResults.Check(Results.ValidationResult, Results.SecuredOperationResult);
            if (aspectResults != null)
            {
                return aspectResults;
            }
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

        public IDataResult<StudyReportDto> CreateStudyReportByDate(DateTime date)
        {
            List<StudyPeriod> studyPeriods = new List<StudyPeriod>();
            var result = GetStudyPeriodsByDate(date);
            double totalStudiedMinutes = 0;

            foreach (var studyPeriod in result.Data)
            {
                studyPeriods.Add(studyPeriod);
                totalStudiedMinutes += (studyPeriod.EndTime - studyPeriod.StartingTime).TotalMinutes;
            }

            return new SuccessDataResult<StudyReportDto>(new StudyReportDto
            {
                StudyPeriods = studyPeriods,
                TotalNumberOfStudiedDays = 1,
                TotalNumberOfStudiedHours = TimeSpan.FromMinutes(totalStudiedMinutes)
            });
        }

        public IDataResult<StudyPredispositionDto> CalculateStudyPredisposition()
        {
            int beforeNoon = 0;
            int afternNoon = 0;
            var result = GetAllStudyPeriods().Data;

            foreach (var item in result)
            {
                if (item.StartingTime < new TimeSpan(12, 0, 0) && item.StartingTime > new TimeSpan(0, 0, 0))
                {
                    beforeNoon++;
                }
                else if (item.StartingTime > new TimeSpan(12, 0, 0) && item.StartingTime < new TimeSpan(23, 59, 59))
                {
                    afternNoon++;
                }
            }

            if (beforeNoon < afternNoon)
            {
                return new SuccessDataResult<StudyPredispositionDto>(new StudyPredispositionDto { studyPredispoisition = "Night" });
            }
            else
            {
                return new SuccessDataResult<StudyPredispositionDto>(new StudyPredispositionDto { studyPredispoisition = "Day" });
            }
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
    }
}
