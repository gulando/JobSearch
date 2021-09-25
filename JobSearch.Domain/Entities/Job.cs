using JobSearch.Domain.Base;

namespace JobSearch.Domain.Entities
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        
        public Company Company { get; set; }

        public Category Category { get; set; }

        public EmploymentType EmploymentType { get; set; }
        
        public Location Location { get; set; }
    }
}