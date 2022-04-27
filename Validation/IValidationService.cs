using System.Collections.Generic;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Lesson3.Validation;

public interface IValidationService<TEntity> where TEntity : class
{
    IReadOnlyList<IOperationFailure> ValidateEntity(TEntity item);
}