using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyPeriodsController : ControllerBase
    {
        IStudyPeriodService _studyPeriodService;

        public StudyPeriodsController(IStudyPeriodService studyPeriodService)
        {
            _studyPeriodService = studyPeriodService;
        }

        [HttpPost("add")]
        public IActionResult Add(StudyPeriod studyPeriod)
        {
            var result = _studyPeriodService.AddStudyPeriod(studyPeriod);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(StudyPeriod studyPeriod)
        {
            var result = _studyPeriodService.DeleteStudyPeriod(studyPeriod);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(StudyPeriod studyPeriod)
        {
            var result = _studyPeriodService.UpdateStudyPeriod(studyPeriod);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _studyPeriodService.GetAllStudyPeriods();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbydate")]
        public IActionResult GetByDate(DateTime date)
        {
            var result = _studyPeriodService.GetStudyPeriodsByDate(date);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbydaterange")]
        public IActionResult GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var result = _studyPeriodService.GetStudyPeriodsByDateRange(startDate, endDate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbymonth")]
        public IActionResult GetByMonth(int month)
        {
            var result = _studyPeriodService.GetStudyPeriodsByMonth(month);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyweek")]
        public IActionResult GetByWeek()
        {
            var result = _studyPeriodService.GetStudyPeriodsByWeek();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyyear")]
        public IActionResult GetByYear(int year)
        {
            var result = _studyPeriodService.GetStudyPeriodsByYear(year);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("createreportbydaterange")]
        public IActionResult CreateReportByDateRange(DateTime startDate, DateTime endDate)
        {
            var result = _studyPeriodService.CreateStudyReportByDateRange(startDate, endDate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("createreportbydate")]
        public IActionResult CreateReportByDate(DateTime date)
        {
            var result = _studyPeriodService.CreateStudyReportByDate(date);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("calculatestudypredisposition")]
        public IActionResult CalculateStudyPredisposition()
        {
            var result = _studyPeriodService.CalculateStudyPredisposition();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
