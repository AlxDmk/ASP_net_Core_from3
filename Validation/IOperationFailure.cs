namespace Lesson3.Validation;

public interface IOperationFailure
{
    string PropertyName { get; }
    string Description { get; }
    string Code { get; }
    
}