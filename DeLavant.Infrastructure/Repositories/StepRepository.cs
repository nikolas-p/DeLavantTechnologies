using DeLavant.Domain.Courses;
using DeLavant.Domain.Steps;
using DeLavant.Infrastructure.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeLavant.Infrastructure.Repositories
{
    public class StepRepository : BaseMongoRepository<Step>, IStepRepository
    {
        public StepRepository(MongoContext context)
       : base(context.Steps) { }


        public async Task<List<Step>> GetStepsByIdsAsync(List<string> stepIds)
        {
            // Конвертируем строки в ObjectId для безопасного поиска
            var objectIds = stepIds
                .Where(id => ObjectId.TryParse(id, out _)) // фильтруем некорректные Id
                .Select(id => new ObjectId(id))
                .ToList();

            // Ищем все Step, где Id входит в список
            var filter = Builders<Step>.Filter.In(s => s.Id, objectIds.Select(oid => oid.ToString()));

            return await _collection.Find(filter).ToListAsync();
        }

        public Task<Step?> GetStepByIdAsync(string id)
            => GetByIdAsync(id);

        public Task CreateStepAsync(Step step)
            => CreateAsync(step);

        public Task UpdateStepAsync(Step step)
            => UpdateAsync(step);

        public Task DeleteStepAsync(string id)
            => DeleteAsync(id);
    }

}

