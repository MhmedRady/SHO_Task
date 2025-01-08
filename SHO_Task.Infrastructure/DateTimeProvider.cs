
using SHO_Task.Application.Abstractions;

namespace SHO_Task.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
