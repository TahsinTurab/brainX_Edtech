using AutoMapper;
using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Infrastructure.Services;
using brainX.Models;
using brainX.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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
                if (Question.isAnonymous)
                {
                    Question.UserName = "Anonymous User";
                    Question.UserPhotoUrl = null;
                }
                else
                {
                    Question.UserName = question.UserName;
                    Question.UserPhotoUrl = question.UserPhotoUrl;
                }
                
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

        public async Task<bool> CreateAnswerAsync(CommunityModel answer, Guid userId, bool isAnonymous)
        {
            try
            {
                var Answer = new CommunityAnswer();
                Answer.UserId = userId;
                Answer.Reply = answer.Description;
                Answer.DateTime = DateTime.Now;
                Answer.UserId = userId;
                Answer.QuestionId = answer.QuestionId;
                if (isAnonymous)
                {
                    Answer.UserName = "Anonymous User";
                    Answer.UserPhotoUrl = null;
                }
                else
                {
                    Answer.UserName = answer.UserName;
                    Answer.UserPhotoUrl = answer.UserPhotoUrl;
                }

                if (answer.Image != null)
                {
                    var result = _fileService.SaveImage(answer.Image);
                    Answer.ImageUrl = result.Item2;
                }

                await _dbContext.Answers.AddAsync(Answer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<ICollection<CommunityQuestion>> GetAllAsync()
        {
            var dbQuestionList = await _dbContext.Questions.ToListAsync();
            return dbQuestionList;
        }

        
        public async Task<ICollection<CommunityAnswer>> GetAllAnswersByIDAsync(Guid id)
        {
            var AnswerList = await _dbContext.Answers.ToListAsync();
            var Answers = new List<CommunityAnswer>();
            foreach(var answer in AnswerList)
            {
                if (answer.QuestionId == id)
                {
                    Answers.Add(answer);
                }
            }
            return Answers;
        }

        public Task<bool> UpdateAsync(CommunityQuestion question)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CommunityAnswer answer)
        {
            throw new NotImplementedException();
        }

        public async Task<CommunityQuestion> GetQuestionByIDAsync(Guid id)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(e => e.Id == id);
            return question;
        }
    }
}
