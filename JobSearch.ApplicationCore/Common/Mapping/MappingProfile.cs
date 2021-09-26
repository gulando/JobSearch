using AutoMapper;
using JobSearch.ApplicationCore.Common.ResponseModels;
using JobSearch.Domain.Entities;

namespace JobSearch.ApplicationCore.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Job, JobResponseModel>()
                .ForMember(s => s.CompanyName, u =>
                    u.MapFrom(x => x.Company.Name))
                .ForMember(s => s.LocationName, u =>
                    u.MapFrom(x => x.Location.Name))
                .ForMember(s => s.CategoryName, u =>
                    u.MapFrom(x => x.Category.Name))
                .ForMember(s => s.EmploymentTypeName, u =>
                    u.MapFrom(x => x.EmploymentType.Name));
        }
    }
}