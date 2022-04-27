using FluentValidation;
using Lesson3.Requests;

namespace Lesson3.Validation.Requests;

public interface ISelectValidator : IValidationService<SelectionRequest>
{
    
}
public class SelectValidator: FluentValidationService<SelectionRequest>, ISelectValidator
{
    public SelectValidator() {
        
            RuleFor(x => x.Skip).NotEmpty().GreaterThan(0).WithErrorCode("Select 1");
            RuleFor(x => x.Take).NotEmpty().GreaterThan(0).WithErrorCode("Select 2");
       
    }
}