using System.Collections.Generic;
using System.Threading.Tasks;
using JobSearch.ApplicationCore.Common.Abstractions.Repositories;
using JobSearch.Domain.Entities;
using JobSearch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearch.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly JobSearchContext _context;
        private readonly ILogger<JobRepository> _logger;
        
        public JobRepository( JobSearchContext dbContext, ILogger<JobRepository> logger)
        {
            _context = dbContext;
            _logger = logger;
        }
        
        public async Task<List<Job>> GetAll()
        {
            var jobs = await _context.Set<Job>()
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Company)
                .Include(x => x.Location)
                .Include(x => x.EmploymentType).ToListAsync();

            return jobs;
        }
    }
}