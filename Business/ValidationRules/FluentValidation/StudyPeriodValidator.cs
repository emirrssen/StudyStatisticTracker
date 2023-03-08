using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class StudyPeriodValidator : AbstractValidator<StudyPeriod>
    {
        public StudyPeriodValidator()
        {
            RuleFor(s => s.StartingTime).NotEmpty();
            RuleFor(s => s.EndTime).NotEmpty();
            RuleFor(s => s.DateOfStudyPeriod).NotEmpty();
            RuleFor(s => s.Title).NotEmpty();
            RuleFor(s => s.Title).MinimumLength(2);
        }
    }
}
