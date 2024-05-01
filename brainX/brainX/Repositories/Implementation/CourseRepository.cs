using AutoMapper;
using brainX.Areas.Instructor.Models;
using brainX.Areas.Student.Models;
using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Infrastructure.Services;
using brainX.Models;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using static System.Net.Mime.MediaTypeNames;

namespace brainX.Repositories.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseRepository(ApplicationDbContext dbContext, 
            IMapper mapper, UserManager<ApplicationUser> userManager,
            IFileService fileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
            _userManager = userManager;
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

        public async Task<CourseCreateModel> GetCourseByIdAsync(Guid id)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(e => e.Id == id);
            var courseUpdateModel = new CourseCreateModel();
            //courseUpdateModel.CourseModel = course;
            courseUpdateModel = _mapper.Map<CourseCreateModel>(course);
            //courseUpdateModel.ContentModels = (List<Content>)course.Contents;
            courseUpdateModel.CategoryList = await GetAllCategoriesAsync();
            return courseUpdateModel;
        }

        public async Task<Course> GetCourseIdAsync(Guid id)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(e => e.Id == id);
            return course;
        }

        public async Task<IList<Content>> GetAllContentsOfCourse(Guid id)
        {
            var contentList = new List<Content>();
            var dbContents = await _dbContext.Contents.OrderBy(item => item.ContentNo).ToListAsync();
            if (dbContents != null)
            {
                foreach (var content in dbContents)
                {
                    if (content.CourseId == id)
                    {
                        contentList.Add(content);
                    }
                }
            }
            
            return contentList; ;
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

        public async Task<IList<Course>> GetAllCourseOfStudentAsync(Guid id)
        {
            var studentCourses = await _dbContext.StudentCourses.Where(p => p.StudentId == id).ToListAsync();
            var allCourses = new List<Course>();
            foreach(var c in studentCourses)
            {
                var course = await _dbContext.Courses.FirstOrDefaultAsync(e => e.Id == c.CourseId);
                allCourses.Add(course);
            }
            return allCourses;
        }

        public Task<bool> GetbyIdAsync(Guid id)
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
                if (contentCreateModel.VideoFiles!=null && contentCreateModel.VideoFiles.Count>=i && contentCreateModel.VideoFiles[i] != null)
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
                content.ContentNo = contentCreateModel.ContentNumbers[i];
                await _dbContext.Contents.AddAsync(content);
                await _dbContext.SaveChangesAsync();
                course.Contents.Add(content);
                _dbContext.Update(course);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task<ContentUpdateModel> GetContentsOfCourseById(Guid Id)
        {
            var allContents = await _dbContext.Contents.OrderBy(item => item.ContentNo).ToListAsync();
            var contents = new List<Content>();
            foreach(var content in allContents)
            {
                if(content.CourseId == Id)
                {
                    contents.Add(content);
                }
            }
            
            var returnModel = new ContentUpdateModel();
            returnModel.Id = Id;
            returnModel.oldContent = contents;
            return returnModel;
        }
        public async Task<bool> UpdateAsync(CourseCreateModel course)
        {
            var dbCourse = await _dbContext.Courses.FirstOrDefaultAsync(e => e.Id == course.Id);
            if (dbCourse == null)
            {
                return false;
            }
            else
            {
                dbCourse.Title = course.Title;
                dbCourse.Category = course.Category;
                dbCourse.Difficulities = course.Difficulities;
                dbCourse.Description = course.Description;
                dbCourse.Fee = course.Fee;
                if (course.ThumbnailFile != null)
                {
                    var result = _fileService.SaveImage(course.ThumbnailFile);
                    if (result.Item1 == 1)
                    {
                        var oldImage = dbCourse.ThumbnailUrl;
                        dbCourse.ThumbnailUrl = result.Item2;
                        if (oldImage != null)
                        {
                            var deleteResult = _fileService.DeleteImage(oldImage);
                        }
                    }
                }
                _dbContext.Update(dbCourse);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdateContentAsync(ContentUpdateModel contentUpdateModel)
        {
            try
            {
                var content = await _dbContext.Contents.FirstOrDefaultAsync(e => e.Id == contentUpdateModel.contentId);

                if(content == null)
                {
                    return false;
                }

                if(contentUpdateModel.ContentName != null)
                {
                    content.ContentName = contentUpdateModel.ContentName;
                }

                if(contentUpdateModel.VideoFiles != null)
                {
                    var result = _fileService.SaveVideo(contentUpdateModel.VideoFiles);
                    content.VideoUrl = result.Item2;
                }

                if(contentUpdateModel.NoteFiles != null)
                {
                    var result = _fileService.SaveNote(contentUpdateModel.NoteFiles);
                    content.NoteUrl = result.Item2;
                }

                _dbContext.Update(content);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<CourseDetailsModel> GetCourseDetailsbyId(Guid Id)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(e => e.Id == Id);
            var courseDetails = _mapper.Map<CourseDetailsModel>(course);
            var instructor = await _userManager.FindByIdAsync(course.InstructorId.ToString());
            courseDetails.InstructorName = instructor.FirstName + " " + instructor.LastName;
            courseDetails.InstructorId = instructor.Id;
            courseDetails.InstructorImageUrl = instructor.ImageUrl;

            var allContents = await _dbContext.Contents.OrderBy(item => item.ContentNo).ToListAsync();
            courseDetails.ContentsList = new List<string>();
            foreach (var content in allContents)
            {
                if (content.CourseId == Id)
                {
                    courseDetails.ContentsList.Add(content.ContentName);
                }
            }
            courseDetails.Students = 0;
            var studentCourses = await _dbContext.StudentCourses.Where(p => p.CourseId == Id).ToListAsync();

            if (studentCourses != null)
            {
                courseDetails.Students = studentCourses.Count;
            }

            if(course.Reviews != null)
            {
                foreach (var review in courseDetails.Reviews)
                {
                    courseDetails.AverageRating += review.Rating;
                }
                courseDetails.AverageRating /= courseDetails.Reviews.Count;
            }

            return courseDetails;
        }

        public async Task<bool> CreateQuestionAsync(TestCreateModel testCreateModel, Guid authorId)
        {
            if (testCreateModel == null)
            {
                return false;
            }
            var test = new Test();
            test.Id = Guid.NewGuid();
            test.AuthorId = authorId;
            test.CourseId = testCreateModel.CourseId;
            test.Name = testCreateModel.Name;
            test.TotalTime = testCreateModel.TotalTime;
            test.PracticalTask1 = testCreateModel.PracticalTask1;
            test.PracticalTask2 = testCreateModel.PracticalTask2;
            test.PracticalTask3 = testCreateModel.PracticalTask3;
            
            await _dbContext.Tests.AddAsync(test);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Test> GetTestByIdAsync(Guid Id)
        {
            var test = await _dbContext.Tests.FirstOrDefaultAsync(e => e.CourseId == Id);
            return test;
        }

        public async Task<Solution> GetSolutionAsync(Guid TestId, Guid StudentId)
        {
            var dbSolutionList = await _dbContext.Solutions.ToListAsync();
            dbSolutionList = dbSolutionList.OrderByDescending(obj => obj.EndingDate).ToList();

            foreach (var sol in dbSolutionList)
            {
                if(sol.TestId == TestId && sol.StudentId == StudentId)
                {
                    return sol;
                    break;
                }
            }
            return null;
        }

        public async Task<bool> CreateSolutionAsync(TakeTestModel takeTestModel)
        {
            var solution = new Solution();
            solution.Id = Guid.NewGuid();
            solution.TestId = takeTestModel.TestId;
            solution.InstructorId = takeTestModel.InstructorId;
            solution.StudentId = takeTestModel.StudentId;
            solution.EndingDate = DateTime.Now;
            solution.Attemp = takeTestModel.Attemp;
            solution.Solution1 = takeTestModel.Solution1;
            solution.Solution2 = takeTestModel.Solution2;
            solution.Solution3 = takeTestModel.Solution3;
            solution.verdict = "Pending";
            await _dbContext.Solutions.AddAsync(solution);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
