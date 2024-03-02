using brainX.Infrastructure.Domains;

namespace brainX.Repositories.Interface
{
    public interface IStudentRepository
    {
        Task CreateAsync(Guid id, string userName);
    }
}
