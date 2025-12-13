using DeLavant.Domain.Steps;
using DeLavant.Domain.Tests;

namespace DeLavant.Application.Tests
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository ?? throw new ArgumentNullException(nameof(testRepository));
        }

        public async Task<Test?> GetTestByIdAsync(string id)
        {
            return await _testRepository.GetTestByIdAsync(id);
        }
        public async Task CreateTestAsync(Test test)
        {
            await _testRepository.CreateTestAsync(test);
        }
        public async Task UpdateTestAsyc(Test test)
        {
            await _testRepository.UpdateTestAsync(test);
        }
        public async Task DeleteTestAsync(string id)
        {
            await _testRepository.DeleteTestAsync(id);
        }
    }
    public class QuestionService : IQuestionService
    {

        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        }
        public async Task CreateQuestionAsync(Question question)
        {
            await _questionRepository.CreateQuestionAsync(question);
        }

        public async Task<Question?> GetQuestionByIdAsync(string id)
        {
            return await _questionRepository.GetQuestionByIdAsync(id);
        }

        public async Task<List<Question>> GetQuestionsByIdsAsync(List<string>? ids)
        {
            if (ids == null || ids.Count == 0) return new List<Question>();
            return await _questionRepository.GetQuestionsByIdsAsync(ids);
        }

        public Task UpdateQuestionAsync(Question question)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteQuestionAsync(string id)
        {
            await _questionRepository.DeleteQuestionAsync(id);
        }
    }
}
