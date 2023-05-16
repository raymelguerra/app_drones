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
                if (!Regex.Match(value, @"^[a-zA-Z0-9_-]+$").Success)
                {
                    context.AddFailure("This field is not valid ( Allowed only letters, numbers, -, _ )");
                }
            }); 
            RuleFor(x => x.Weight).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.Code).Custom((value, context) =>
            {
                if (!Regex.Match(value, @"^[A-Z_\d]+$").Success) {
                    context.AddFailure("This field is not valid ( Allowed only upper case letters, underscore and numbers )");
                } 
            });
            RuleFor(x => x.Image).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.Image).Custom((value, context) =>
            {
                if (!IsBase64String(value.Replace("data:image/png;base64,","")))
                {
                    context.AddFailure("This image is not a valid base64 string");
                }
            });
        }

        public static bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }
}
