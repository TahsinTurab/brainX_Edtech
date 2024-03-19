using brainX.Areas.Instructor.Models;
using brainX.Infrastructure.Domains;
using brainX.Models;

namespace brainX.Repositories.Interface
{
    public interface ICommunityRespsitory
    {
        Task<ICollection<Course>> GetAllAsync();
        Task<bool> UpdateAsync(CommunityQuestion question);
        Task<bool> UpdateAsync(CommunityAnswer answer);
        Task<bool> CreateAsync(CommunityModel question, Guid userId, bool isAnonymous);
        Task<string> CreateAsync(CommunityAnswer answer, Guid userId, Guid questionId);
    }
}
