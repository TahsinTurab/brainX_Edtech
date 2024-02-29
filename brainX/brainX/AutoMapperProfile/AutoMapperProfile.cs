using AutoMapper;
using brainX.Areas.Instructor.Models;
using brainX.Infrastructure.Domains;

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
        }
    }
}
