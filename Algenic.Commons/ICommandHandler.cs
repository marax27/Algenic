using System.Threading.Tasks;

namespace Algenic.Commons
{
    public interface ICommandHandler<in TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
