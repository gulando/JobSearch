using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JobSearch.ApplicationCore.Common.Error;
using JobSearch.ApplicationCore.Common.ResponseModels;
using JobSearch.ApplicationCore.UseCases.Queries.Job;
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
        private readonly IMapper _mapper;

        public JobController(IMapper mapper, ILogger<JobController> logger, IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            var request = new GetJobModel();

            var jobs = await _mediator.Send(request, cancellationToken);

            var responseModel = _mapper.Map<List<JobResponseModel>>(jobs);

            return Ok(responseModel);
        }
    }
}