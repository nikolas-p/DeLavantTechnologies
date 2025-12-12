using DeLavant.Domain.Abstractions;
using MongoDB.Driver;

namespace DeLavant.Infrastructure.Repositories
{
    public class BaseMongoRepository<T> where T : IEntity
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseMongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public virtual async Task<T> GetByIdAsync(string id)
            => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public virtual async Task<List<T>> GetObjectsByIdsAsync(IEnumerable<string> ids)
       => await _collection.Find(x => ids.Contains(x.Id)).ToListAsync();

        public virtual async Task<List<T>> GetAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public virtual async Task CreateAsync(T entity)
            => await _collection.InsertOneAsync(entity);

        public virtual async Task UpdateAsync(T entity)
            => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

        public virtual async Task DeleteAsync(string id)
            => await _collection.DeleteOneAsync(x => x.Id == id);


    }

}
