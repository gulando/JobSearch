using System.Collections.Generic;
using JobSearch.Domain.Base;

namespace JobSearch.Domain.Entities
{
    public class EmploymentType : BaseEntity
    {
        public string Type { get; set; }
        
        public List<Job> Jobs { get; set; }
    }
}