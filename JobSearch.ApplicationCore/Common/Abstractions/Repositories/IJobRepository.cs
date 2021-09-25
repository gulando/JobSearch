using System.Collections.Generic;
using System.Threading.Tasks;
using JobSearch.Domain.Entities;

namespace JobSearch.ApplicationCore.Common.Abstractions.Repositories
{
    public interface IJobRepository
    {
        Task<List<Job>> GetAll();
    }
}