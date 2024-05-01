using brainX.Areas.Instructor.Models;
using brainX.Areas.Student.Models;
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
        Task<Test> GetTestByIdAsync(Guid Id);
        Task<Solution> GetSolutionAsync(Guid TestId, Guid StudentId);
        Task<List<Solution>> GetAllSolutionOfInstructor(Guid instructorId);
        Task<bool> CreateSolutionAsync(TakeTestModel takeTestModel);
        Task<Test> GetTestByTestIdAsync(Guid Id);
        Task<Solution> GetSolutionByIdAsync(Guid Id);
        Task UpdateSolutionAsync(EvaluationModel model);
        Task<Guid> CreateCourseAsync(AIModel model);

    }
}
