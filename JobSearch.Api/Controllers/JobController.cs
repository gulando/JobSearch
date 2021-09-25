using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearch.ApplicationCore.UseCases.Queries.Job;
using JobSearch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobSearch.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IMediator _mediator;

        public JobController(ILogger<JobController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Job>> Get(CancellationToken cancellationToken = default)
        {
            var request = new GetJobModel();

            var jobs = await _mediator.Send(request, cancellationToken);

            return jobs;
        }
    }
}