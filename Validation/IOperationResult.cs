using System.Collections.Generic;

namespace Lesson3.Validation;

public interface IOperationResult<TResult>
{
    
    IReadOnlyList<IOperationFailure> Failures { get; }
    bool Succeed { get; }
}