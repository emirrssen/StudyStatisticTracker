using Core.Constants;
using Core.Utilities.Results;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation.FluentValidation
{
    public class ValidationTool
    {
        public static IResult Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);

            if (!result.IsValid)
            {
                return new ErrorResult(result.ToString());
                //Results.ValidationResult = new ErrorResult(result.ToString());
            } else
            {
                return new SuccessResult();
                //Results.ValidationResult = new SuccessResult();
            }
        }
    }
}
