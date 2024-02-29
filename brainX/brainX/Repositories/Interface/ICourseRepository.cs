using brainX.Areas.Instructor.Models;
using brainX.Infrastructure.Domains;

namespace brainX.Repositories.Interface
{
    public interface ICourseRepository
    {
        Task<bool> CreateAsync(CourseCreateModel course, Guid instructorId);
        Task<bool> GetbyIdAsync(Guid id);
        Task<ICollection<Course>> GetAllAsync();
        Task<bool> UpdateAsync(Course course);
        Task<List<string>> GetAllCategoriesAsync();
    }
}
