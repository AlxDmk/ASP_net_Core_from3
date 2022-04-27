using FluentValidation;
using Lesson3.Requests.PersonRequests;

namespace Lesson3.Validation.Requests;

public interface IPersonByIdValidator : IValidationService<PersonByIdRequest>
{
    
}

public class PersonByIdValidator: FluentValidationService<PersonByIdRequest>, IPersonByIdValidator
{
    public PersonByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithErrorCode("Person 1");
    }
}