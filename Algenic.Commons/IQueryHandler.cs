using System.Threading.Tasks;

namespace Algenic.Commons
{
    public interface IQueryHandler<in TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
