using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class StudyReportDto : IDto
    {
        public List<StudyPeriod> StudyPeriods { get; set; }
        public TimeSpan TotalNumberOfStudiedHours { get; set; }
        public TimeSpan AverageNumberOfStudiedHours { get; set; }
        public int TotalNumberOfStudiedDays { get; set; }
    }
}
