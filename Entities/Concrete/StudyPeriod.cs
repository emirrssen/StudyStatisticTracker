using Core.Entities;
using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class StudyPeriod : IEntity
    {
        public int Id { get; set; }
        public DateTime DateOfStudyPeriod { get; set; }
        public TimeSpan StartingTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Title { get; set; }
        public string? Annotation { get; set; }
    }
}
