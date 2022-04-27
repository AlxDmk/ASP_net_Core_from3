using System.Collections.Generic;

namespace Lesson3.Validation;

public class OperationResult<TResult>:IOperationResult<TResult>
{
    
    public IReadOnlyList<IOperationFailure> Failures { get; }
    public bool Succeed { get; }

    public OperationResult(IReadOnlyList<IOperationFailure> failures)
    {
        Failures = failures is null ? new List<IOperationFailure>(): failures;
        Succeed = Failures.Count == 0;
    }
    
}