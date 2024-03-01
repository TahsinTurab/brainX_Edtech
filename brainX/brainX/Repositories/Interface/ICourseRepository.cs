using brainX.Areas.Instructor.Models;
using brainX.Infrastructure.Domains;

namespace brainX.Repositories.Interface
{
    public interface ICourseRepository
    {
        Task<string> CreateAsync(CourseCreateModel course, Guid instructorId);
        Task<bool> GetbyIdAsync(Guid id);
        Task<ICollection<Course>> GetAllAsync();
        Task<bool> UpdateAsync(Course course);
        Task<List<string>> GetAllCategoriesAsync();
        Task<bool> CreateContentsAsync(ContentCreateModel contentCreateModel);
        Task<ICollection<Course>> GetAllAsync(Guid id);
    }
}
