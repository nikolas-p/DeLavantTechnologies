using DeLavantTechnologies.Data;
using MongoDB.Driver;

namespace DeLavantTechnologies.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly IMongoCollection<Position> _collection;

        public PositionRepository(IMongoDbContext context)
        {
            _collection = context.GetCollection<Position>("Positions");
        }

        public async Task<List<Position>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<Position?> GetByIdAsync(Guid id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Position entity) =>
            await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(Position entity) =>
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

        public async Task DeleteAsync(Guid id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
