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
                
                var course = await _dbContext.Courses.FirstOrDefaultAsync(e => e.Id == courseId);
                var instructorId = course.InstructorId;
                var account = await _dbContext.Accounts.FirstOrDefaultAsync(e => e.InstructorId == instructorId);
                if(account.TotalRevenue == null)
                {
                    account.TotalRevenue = 0;
                }
                account.TotalRevenue += course.Fee;
                if (account.CurrentBalance == null)
                {
                    account.CurrentBalance = 0;
                }
                account.CurrentBalance += course.Fee;
                await _dbContext.StudentCourses.AddAsync(model);
                _dbContext.Update(account);
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
