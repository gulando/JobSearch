using System.Collections.Generic;
using JobSearch.Domain.Base;

namespace JobSearch.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        
        public List<Job> Jobs { get; set; }
    }
}