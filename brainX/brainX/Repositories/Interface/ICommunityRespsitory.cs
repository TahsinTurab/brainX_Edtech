using brainX.Areas.Instructor.Models;
using brainX.Infrastructure.Domains;
using brainX.Models;

namespace brainX.Repositories.Interface
{
    public interface ICommunityRespsitory
    {
        Task<ICollection<CommunityQuestion>> GetAllAsync();
        Task<ICollection<CommunityAnswer>> GetAllAnswersByIDAsync(Guid id);
        Task<CommunityQuestion> GetQuestionByIDAsync (Guid id);
        Task<bool> UpdateAsync(CommunityQuestion question);
        Task<bool> UpdateAsync(CommunityAnswer answer);
        Task<bool> CreateAsync(CommunityModel question, Guid userId, bool isAnonymous);
        Task<bool> CreateAnswerAsync(CommunityModel answer, Guid userId, bool isAnonymous);
    }
}
