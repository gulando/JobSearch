using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearch.ApplicationCore.Common.Abstractions.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobSearch.ApplicationCore.UseCases.Queries.Job
{
    public class JobHandler : IRequestHandler<GetJobModel, List<Domain.Entities.Job>>
    {
        private readonly IJobRepository _repository;
        private readonly ILogger<JobHandler> _logger;
        
        public JobHandler(IJobRepository repository, ILogger<JobHandler>  logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public async Task<List<Domain.Entities.Job>> Handle(GetJobModel request, CancellationToken cancellationToken)
        {
            var jobs = await _repository.GetAll();

            return jobs;
        }
    }
}