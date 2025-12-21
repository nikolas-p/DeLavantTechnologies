using DeLavant.Domain.Steps;
using DeLavant.Domain.Tests;

namespace DeLavant.Application.Tests
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IQuestionService _questionService;

        public TestService(
            ITestRepository testRepository,
            IQuestionService questionService)
        {
            _testRepository = testRepository
                ?? throw new ArgumentNullException(nameof(testRepository));

            _questionService = questionService
                ?? throw new ArgumentNullException(nameof(questionService));
        }

        /* ================== GET ================== */

        public async Task<Test?> GetTestByIdAsync(string id)
        {
            return await _testRepository.GetTestByIdAsync(id);
        }

        /* ================== CREATE ================== */

        public async Task CreateTestAsync(Test test)
        {
            await _testRepository.CreateTestAsync(test);
        }

        /* ================== UPDATE ================== */
        public async Task SaveTestAsync(Test test)
        {
            if (test == null)
                throw new ArgumentNullException(nameof(test));

            var existing = string.IsNullOrEmpty(test.Id)
                ? null
                : await _testRepository.GetTestByIdAsync(test.Id);

            test.Questions ??= new List<string>();

            // Удаляем вопросы, которых больше нет
            if (existing?.Questions != null)
            {
                var removed = existing.Questions
                    .Where(q => !test.Questions.Contains(q))
                    .ToList();

                foreach (var qId in removed)
                    await _questionService.DeleteQuestionAsync(qId);
            }

            // Сохраняем или обновляем вопросы
            foreach (var questionId in test.Questions)
            {
                var question = await _questionService.GetQuestionByIdAsync(questionId) ?? new Question { Id = questionId };
                await _questionService.SaveQuestionAsync(question);
            }

            // Сохраняем или обновляем сам тест
            if (existing == null)
                await _testRepository.CreateTestAsync(test);
            else
                await _testRepository.UpdateTestAsync(test);
        }



        public async Task UpdateTestAsync(Test updatedTest)
        {
            var existing = await _testRepository.GetTestByIdAsync(updatedTest.Id);
            if (existing == null)
                throw new InvalidOperationException("Test not found");

            var existingQuestions = existing.Questions ?? new List<string>();
            var updatedQuestions = updatedTest.Questions ?? new List<string>();

            // 1️⃣ Найти удалённые вопросы
            var removedQuestions = existingQuestions
                .Where(q => !updatedQuestions.Contains(q))
                .ToList();

            // 2️⃣ КАСКАДНО удалить вопросы
            foreach (var questionId in removedQuestions)
            {
                await _questionService.DeleteQuestionAsync(questionId);
            }

            // 3️⃣ Обновить тест
            await _testRepository.UpdateTestAsync(updatedTest);
        }

        /* ================== DELETE ================== */

        public async Task DeleteTestAsync(string id)
        {
            var test = await _testRepository.GetTestByIdAsync(id);
            if (test == null)
                return;

            // 1️⃣ Удаляем все вопросы теста
            if (test.Questions != null)
            {
                foreach (var questionId in test.Questions)
                {
                    await _questionService.DeleteQuestionAsync(questionId);
                }
            }

            // 2️⃣ Удаляем сам тест
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
        public async Task SaveQuestionAsync(Question question)
        {
            question.Answers ??= new List<Answer>();

            // Проверяем, есть ли вопрос в БД
            var existing = await _questionRepository.GetQuestionByIdAsync(question.Id);
            if (existing == null)
                await _questionRepository.CreateQuestionAsync(question);
            else
                await _questionRepository.UpdateQuestionAsync(question);
        }



        public async Task DeleteQuestionAsync(string id)
        {
            await _questionRepository.DeleteQuestionAsync(id);
        }
    }
}
