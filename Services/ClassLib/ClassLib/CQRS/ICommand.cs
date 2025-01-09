using MediatR;

namespace ClassLib.CQRS
{
    public interface ICommand : ICommand<Unit>
    {

    }
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {

    }
}
