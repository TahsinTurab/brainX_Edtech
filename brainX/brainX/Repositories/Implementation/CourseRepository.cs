using AutoMapper;
using brainX.Areas.Instructor.Models;
using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Infrastructure.Services;
using brainX.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace brainX.Repositories.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CourseRepository(ApplicationDbContext dbContext, IMapper mapper, IFileService fileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
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

        public async Task<bool> CreateAsync(CourseCreateModel courseModel, Guid instructorId)
        {
            if (courseModel == null)
            {
                return false;
            }

            //Image Upload
            if (courseModel.ThumbnailFile != null)
            {
                var result = _fileService.SaveImage(courseModel.ThumbnailFile);
                courseModel.ThumbnailUrl = result.Item2;
            }
            courseModel.CreationDate = DateOnly.FromDateTime(DateTime.Today);
            var instructor = await _dbContext.Instructors.FirstOrDefaultAsync(e => e.Id == instructorId);
            var course = _mapper.Map<Course>(courseModel);
            course.Id = Guid.NewGuid();
            course.InstructorId = instructorId;
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
            instructor.Courses.Add(course);
            _dbContext.Update(instructor);
            await _dbContext.SaveChangesAsync();
            return true;
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
