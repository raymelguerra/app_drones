using AppDrones.Core.Dto;
using FluentValidation;

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
