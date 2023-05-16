using AppDrones.Core.Dto;
using AppDrones.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Validations
{
    public class LoadingMedicationValidator : AbstractValidator<IEnumerable<LoadMedicationReqDto>>
    {
        public LoadingMedicationValidator()
        {
            RuleForEach(x => x).SetValidator(new MedicationValidator());
        }
    }
}
