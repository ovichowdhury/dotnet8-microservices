using MediatR;


namespace ClassLib.CQRS
{
    public interface TCommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {

    }
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }
}
