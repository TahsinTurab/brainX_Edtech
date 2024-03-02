using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace brainX.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Guid id, string userName)
        {
            var dbStudent = await _dbContext.Students.FirstOrDefaultAsync(e => e.Id == id);
            if (dbStudent == null)
            {
                var student = new Student();
                student.Id = id;
                student.UserName = userName;
                await _dbContext.Students.AddAsync(student);
                await _dbContext.SaveChangesAsync();
            }
            return;
        }

        public async Task<bool> EnrollCourseAsync(Guid studentId, Guid courseId)
        {
            try
            {
                var model = new StudentCourse();
                model.Id = Guid.NewGuid();
                model.CourseId = courseId;
                model.StudentId = studentId;
                await _dbContext.StudentCourses.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        } 
    }
}
