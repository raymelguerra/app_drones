using AppDrones.Core.Dto;
using AppDrones.Core.Models;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Validations
{
    public class RegistryDroneValidator : AbstractValidator<RegistryReqDto>
    {
        public RegistryDroneValidator()
        {
            RuleFor(x => x.SerialNumber).Custom((value, context) =>
            {
                if (value.Length > 100)
                    context.AddFailure("This field must have less than 100 characters");
            });
            RuleFor(x => x.SerialNumber).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.Model).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.Model).Custom((value, context) =>
            {
                if (!Enum.IsDefined(typeof(Model), value))
                    context.AddFailure("This is not a valid value for this field. Possible values ​​are: Lightweight, Middleweight, Cruiserweight, Heavyweight");
            });
            RuleFor(x => x.WeightLimit).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.WeightLimit).Custom((value, context) =>
            {
                if (value > 500)
                    context.AddFailure("The weight limit must be less than 500 gr");
            });
            RuleFor(x => x.BatteryCapacity).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.BatteryCapacity).Custom((value, context) =>
            {
                if (value > 100)
                    context.AddFailure("Battery capacity must be less than 100%");
            });
            RuleFor(x => x.State).NotNull().NotEmpty().WithMessage("This field is mandatory");
            RuleFor(x => x.State).Custom((value, context) =>
            {
                if (!Enum.IsDefined(typeof(State), value))
                    context.AddFailure("This is not a valid value for this field. Possible values ​​are: IDLE, LOADING, LOADED, DELIVERING, DELIVERED, RETURNING");
            });
        }
    }
}
