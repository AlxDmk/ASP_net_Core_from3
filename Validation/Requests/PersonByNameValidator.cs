using System.Collections.Generic;
using FluentValidation;
using Lesson3.Requests.PersonRequests;

namespace Lesson3.Validation.Requests;

public interface IPersonByNameValidator : IValidationService<PersonByNameRequest>
{
    
}
    
public class PersonByNameValidator : FluentValidationService<PersonByNameRequest>, IPersonByNameValidator
{
    public PersonByNameValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 15).Matches("^[a-zA-Z]*$").WithErrorCode("Person 2");
    }
}