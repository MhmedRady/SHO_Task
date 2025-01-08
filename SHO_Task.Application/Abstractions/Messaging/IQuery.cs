using MediatR;

namespace SHO_Task.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
