namespace Algenic.Commons
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}
