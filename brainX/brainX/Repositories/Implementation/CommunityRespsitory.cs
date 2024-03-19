using AutoMapper;
using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Infrastructure.Services;
using brainX.Models;
using brainX.Repositories.Interface;

namespace brainX.Repositories.Implementation
{
    public class CommunityRespsitory : ICommunityRespsitory
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CommunityRespsitory(ApplicationDbContext dbContext, IMapper mapper, IFileService fileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> CreateAsync(CommunityModel question, Guid userId, bool isAnonymous)
        {
            try
            {
                var Question = new CommunityQuestion();
                Question.UserId = userId;
                Question.Title = question.Title;
                Question.Details = question.Description;
                Question.DateTime = DateTime.Now;
                Question.UserId = userId;
                Question.isAnonymous = isAnonymous;
                if (question.Image != null)
                {
                    var result = _fileService.SaveImage(question.Image);
                    Question.ImageUrl = result.Item2;
                }

                await _dbContext.Questions.AddAsync(Question);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<string> CreateAsync(CommunityAnswer answer, Guid userId, Guid questionId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Course>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CommunityQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CommunityAnswer answer)
        {
            throw new NotImplementedException();
        }
    }
}
