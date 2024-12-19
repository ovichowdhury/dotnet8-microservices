using MediatR;

namespace ClassLib.CQRS
{
    public interface IQuery<TResponse> : IRequest<TResponse>
        where TResponse : notnull
    {
    }
}
