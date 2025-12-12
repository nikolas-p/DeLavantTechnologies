using DeLavant.Domain.Steps;

namespace DeLavant.Application.Steps
{
    public class StepService : IStepService
    {
        private readonly IStepRepository _stepRepository;

        // Конструктор с DI
        public StepService(IStepRepository stepRepository)
        {
            _stepRepository = stepRepository ?? throw new ArgumentNullException(nameof(stepRepository));
        }

        public async Task<List<Step>> GetStepsByIdsAsync(List<string>? ids)
        {
            if (ids == null || ids.Count == 0) return new List<Step>();
            return await _stepRepository.GetStepsByIdsAsync(ids);
        }

        public async Task<Step?> GetStepByIdAsync(string id)
        {
            return await _stepRepository.GetStepByIdAsync(id);
        }

        public async Task CreateStepAsync(Step step)
        {
            await _stepRepository.CreateStepAsync(step);
        }

        public async Task UpdateStepAsync(Step step)
        {
            await _stepRepository.UpdateStepAsync(step);
        }

        public async Task DeleteStepAsync(string id)
        {
            await _stepRepository.DeleteStepAsync(id);
        }
    }

}
