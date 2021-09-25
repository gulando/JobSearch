using System.Collections.Generic;
using MediatR;

namespace JobSearch.ApplicationCore.UseCases.Queries.Job
{
    public class GetJobModel : IRequest<List<Domain.Entities.Job>>
    {
        
    }
}