using brainX.Infrastructure.Domains;

namespace brainX.Repositories.Interface
{
    public interface ICourseRepository
    {
        Task CreateAsync(Course course);
        Task<bool> GetbyIdAsync(Guid id);
        Task<ICollection<Course>> GetAllAsync();
        Task<bool> UpdateAsync(Course course);
        Task<List<string>> GetAllCategoriesAsync();
    }
}
