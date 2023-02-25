using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStudyPeriodService
    {
        IResult AddStudyPeriod(StudyPeriod studyPeriod);
        IResult DeleteStudyPeriod(StudyPeriod studyPeriod);
        IResult UpdateStudyPeriod(StudyPeriod studyPeriod);
        IDataResult<List<StudyPeriod>> GetAllStudyPeriods();
        IDataResult<List<StudyPeriod>> GetStudyPeriodsByDate(DateTime date);
        IDataResult<List<StudyPeriod>> GetStudyPeriodsByYear(int year);
        IDataResult<List<StudyPeriod>> GetStudyPeriodsByMonth(int month);
        IDataResult<List<StudyPeriod>> GetStudyPeriodsByWeek();
        IDataResult<List<StudyPeriod>> GetStudyPeriodsByDateRange(DateTime startingDate, DateTime endingDate);
        IDataResult<StudyReportDto> CreateStudyReportByDateRange(DateTime startingDate, DateTime endingDate);
        IDataResult<StudyReportDto> CreateStudyReportByDay(DateTime date);
        IDataResult<StudyPredispositionDto> CalculateStudyPredisposition();
    }
}
