using DeLavant.Domain.Lectures;
using DeLavant.Domain.Steps;
using DeLavant.Domain.Tests;
using DeLavant.Infrastructure.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeLavant.Infrastructure.Repositories
{
    public class TestRepository : BaseMongoRepository<Test>, ITestRepository
    {

        public TestRepository(MongoContext context)
            : base(context.Tests) { }
        public Task CreateTestAsync(Test test) => CreateAsync(test);

        public Task DeleteTestAsync(string id) => DeleteAsync(id);

        public Task<Test?> GetTestByIdAsync(string id) => GetByIdAsync(id);

        public async Task<List<Test>> GetTestsByIdsAsync(List<string> testIds)
        {
            // Конвертируем строки в ObjectId для безопасного поиска
            var objectIds = testIds
                .Where(id => ObjectId.TryParse(id, out _)) // фильтруем некорректные Id
                .Select(id => new ObjectId(id))
                .ToList();

            // Ищем все Test, где Id входит в список
            var filter = Builders<Test>.Filter.In(s => s.Id, objectIds.Select(oid => oid.ToString()));

            return await _collection.Find(filter).ToListAsync();
        }

        public Task UpdateTestAsync(Test test) => UpdateAsync(test);
    }

    public class QuestionRepository : BaseMongoRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(MongoContext context)
          : base(context.Questions) { }
        public Task CreateQuestionAsync(Question question) => CreateAsync(question);

        public Task DeleteQuestionAsync(string id) => DeleteAsync(id);

        public Task<Question?> GetQuestionByIdAsync(string id) => GetByIdAsync(id);

        public async Task<List<Question>> GetQuestionsByIdsAsync(List<string> questionIds)
        {
            // Конвертируем строки в ObjectId для безопасного поиска
            var objectIds = questionIds
                .Where(id => ObjectId.TryParse(id, out _)) // фильтруем некорректные Id
                .Select(id => new ObjectId(id))
                .ToList();

            // Ищем все Step, где Id входит в список
            var filter = Builders<Question>.Filter.In(s => s.Id, objectIds.Select(oid => oid.ToString()));

            return await _collection.Find(filter).ToListAsync();
        }

        public Task UpdateQuestionAsync(Question question) => UpdateAsync(question);


    }
}
