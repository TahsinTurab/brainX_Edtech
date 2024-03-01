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

        public async Task<string> CreateAsync(CourseCreateModel courseModel, Guid instructorId)
        {
            if (courseModel == null)
            {
                return null;
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
            return course.Id.ToString();
        }

        public async Task<ICollection<Course>> GetAllAsync()
        {
            var dbCourseList = await _dbContext.Courses.ToListAsync();
            return dbCourseList;
        }

        public async Task<ICollection<Course>> GetAllAsync(Guid id)
        {
            var courseList = await GetAllAsync();
            var myCourses = new List<Course>();
            foreach (var course in courseList)
            {
                if(course.InstructorId == id)
                {
                    myCourses.Add(course);
                }
            }
            return myCourses;
            
        }

        public Task<bool> GetbyIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Course course)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateContentsAsync(ContentCreateModel contentCreateModel) 
        {
            if(contentCreateModel == null)
            {
                return false;
            }
            var loop = contentCreateModel.ContentNames.Count;
            var course = await _dbContext.Courses.FirstOrDefaultAsync(e => e.Id == contentCreateModel.CourseId);
            for (int i=0; i < loop; i++)
            {
                var tempContent = new ContentCreateModel();
                tempContent.CourseId = contentCreateModel.CourseId;
                tempContent.ContentName = contentCreateModel.ContentNames[i];
                if (contentCreateModel.VideoFiles!=null && contentCreateModel.VideoFiles.Count>=i && contentCreateModel.VideoFiles[i] == null)
                {
                    var result = _fileService.SaveVideo(contentCreateModel.VideoFiles[i]);
                    tempContent.VideoUrl = result.Item2;
                }
                
                if (contentCreateModel.NoteFiles != null && contentCreateModel.NoteFiles.Count >= i && contentCreateModel.NoteFiles[i] != null)
                {
                    var result = _fileService.SaveNote(contentCreateModel.NoteFiles[i]);
                    tempContent.NoteUrl = result.Item2;
                }
                
                var content = _mapper.Map<Content>(tempContent);
                content.Id = Guid.NewGuid();
                await _dbContext.Contents.AddAsync(content);
                await _dbContext.SaveChangesAsync();
                course.Contents.Add(content);
                _dbContext.Update(course);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }

    }
}
