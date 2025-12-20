using DeLavantTechnologies.Data;

namespace DeLavantTechnologies.Repositories
{
    public interface IPositionRepository
    {
        Task<List<Position>> GetAllAsync();
        Task<Position?> GetByIdAsync(Guid id);
        Task CreateAsync(Position entity);
        Task UpdateAsync(Position entity);
        Task DeleteAsync(Guid id);
    }
}
