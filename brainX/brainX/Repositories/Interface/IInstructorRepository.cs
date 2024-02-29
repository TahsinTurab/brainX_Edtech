using brainX.Infrastructure.Domains;
using brainX.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainX.Infrastructure.Repositories.Interface
{
    public interface IInstructorRepository
    {
        Task CreateAsync(Guid instructorId, string userName);
        Task<bool> GetbyIdAsync(Guid id);
        Task<ICollection<Instructor>> GetAllAsync();
        Task<bool> UpdateAsync(Instructor instructor);
    }
}
