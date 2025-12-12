using DeLavant.Domain.Lectures;

namespace DeLavant.Domain.Steps
{
    public interface IStepRepository
    {
        Task<List<Step>> GetStepsByIdsAsync(List<string> StepIds);
        Task<Step?> GetStepByIdAsync(string id);
        Task CreateStepAsync(Step step);
        Task UpdateStepAsync(Step step);
        Task DeleteStepAsync(string id);
    }
}
