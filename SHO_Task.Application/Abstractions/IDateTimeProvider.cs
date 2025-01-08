namespace SHO_Task.Application.Abstractions;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
