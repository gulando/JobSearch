using System.Threading;
using System.Threading.Tasks;

namespace JobSearch.ApplicationCore.Common.Abstractions.DataSeed
{
    public interface IDataSeed
    {
        Task SeedAllInitialDataAsync(CancellationToken cancellationToken = default);

    }
}