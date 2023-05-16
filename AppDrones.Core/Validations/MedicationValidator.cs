using AppDrones.Core.Dto;
using AppDrones.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppDrones.Core.Validations
{
    public class MedicationValidator : AbstractValidator<LoadMedicationReqDto>
    {
        public MedicationValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.Name).Custom((value, context) =>
            {
                // string pattern = @"^[a-zA-Z0-9_-]+$";
                if (!Regex.Match(value, @"^[a-zA-Z0-9_-]+$").Success)
                {
                    context.AddFailure("This field is not valid ( Allowed only letters, numbers, -, _ )");
                }
                //try
                //{
                //    Regex.Match(value, pattern);
                //}
                //catch (ArgumentException)
                //{
                //    context.AddFailure("This field is not valid ( Allowed only letters, numbers, -, _ )");
                //}
            }); 
            RuleFor(x => x.Weight).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.Code).Custom((value, context) =>
            {
                if (!Regex.Match(value, @"^[A-Z_\d]+$").Success) {
                    context.AddFailure("This field is not valid ( Allowed only upper case letters, underscore and numbers )");
                } 
            });
        }
    }
}
