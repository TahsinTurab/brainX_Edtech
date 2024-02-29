using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Infrastructure.DTOs;
using brainX.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace brainX.Infrastructure.Repositories.Implementation
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InstructorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public async Task CreateAsync(Guid instructorId, string userName)
        {
            var dbInstructor = await _dbContext.Instructors.FirstOrDefaultAsync(e => e.Id == instructorId);
            if(dbInstructor == null)
            {
                var instructor = new Instructor();
                instructor.Id = instructorId;
                instructor.UserName = userName;
                instructor.Courses = new List<Course>();
                instructor.Account = new Account();
                await _dbContext.Instructors.AddAsync(instructor);
                await _dbContext.SaveChangesAsync();
            }
            return;
        }

        public Task<ICollection<Instructor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetbyIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Instructor instructor)
        {
            throw new NotImplementedException();
        }
    }
}
