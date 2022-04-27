using FluentValidation;
using Lesson3.Controllers.Models;

namespace Lesson3.Validation.Requests;

public interface IPersonValidator : IValidationService<PersonDto>
{
    
}

public class PersonValidator: FluentValidationService<PersonDto>, IPersonValidator
{
    public PersonValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().Length(2, 15).Matches("^[a-zA-Z]*$").WithErrorCode("Person 3.1");
        RuleFor(x => x.LastName).NotEmpty().Length(2, 15).Matches("^[a-zA-Z]*$").WithErrorCode("Person 3.2");
        RuleFor(x => x.Age).NotEmpty().InclusiveBetween(18, 65).WithErrorCode("Person 3.3");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithErrorCode("Person 3.3");
    }
}