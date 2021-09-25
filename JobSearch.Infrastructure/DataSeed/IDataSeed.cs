using System.Threading;
using System.Threading.Tasks;

namespace JobSearch.Infrastructure.DataSeed
{
    public interface IDataSeed
    {
        Task SeedAllInitialDataAsync(CancellationToken cancellationToken = default);

    }
}