using AutoMapper;
using brainX.Areas.Instructor.Models;
using brainX.Infrastructure.Domains;
using brainX.Models;

namespace brainX.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CourseCreateModel, Course>()
                .ReverseMap();
            CreateMap<ContentCreateModel, Content>()
                .ReverseMap();
            CreateMap<CourseDetailsModel, Course>()
                .ReverseMap();
        }
    }
}
