using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace brainX.Repositories.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetAllCategoriesAsync()
        {
            var CategoryList = new List<string>();
            var dbCategoriyList = await _dbContext.Categories.ToListAsync();
            foreach (var category in dbCategoriyList)
            {
                CategoryList.Add(category.Name);
            }
            return CategoryList.OrderBy(s=>s).ToList();
        }

        public Task CreateAsync(Course course)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Course>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetbyIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
