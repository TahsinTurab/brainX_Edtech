using brainX.Areas.Instructor.Models;
using brainX.Infrastructure.Domains;
using brainX.Models;

namespace brainX.Repositories.Interface
{
    public interface ICourseRepository
    {
        Task<string> CreateAsync(CourseCreateModel course, Guid instructorId);
        Task<bool> GetbyIdAsync(Guid id);
        Task<ICollection<Course>> GetAllAsync();
        Task<bool> UpdateAsync(CourseCreateModel course);
        Task<List<string>> GetAllCategoriesAsync();
        Task<bool> CreateContentsAsync(ContentCreateModel contentCreateModel);
        Task<bool> CreateQuestionAsync(TestCreateModel testCreateModel, Guid authorId);
        Task<ICollection<Course>> GetAllAsync(Guid id);
        Task<CourseCreateModel> GetCourseByIdAsync(Guid id);
        Task<ContentUpdateModel> GetContentsOfCourseById(Guid Id);
        Task<bool> UpdateContentAsync(ContentUpdateModel contentUpdateModel);
        Task<CourseDetailsModel> GetCourseDetailsbyId(Guid Id);
        Task<IList<Course>> GetAllCourseOfStudentAsync(Guid id);
        Task<IList<Content>> GetAllContentsOfCourse(Guid id);
        Task<Course> GetCourseIdAsync(Guid id);

    }
}
