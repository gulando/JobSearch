using JobSearch.Domain.Base;

namespace JobSearch.Domain.Entities
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        
        public int CompanyId { get; set; }

        public Company Company { get; set; }
        
        public int LocationId { get; set; }

        public Location Location { get; set; }
        
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int EmploymentTypeId { get; set; }

        public EmploymentType EmploymentType { get; set; }
    }
}