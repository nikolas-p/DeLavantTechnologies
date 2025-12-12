using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeLavant.Domain.Steps;

namespace DeLavant.Application.Steps
{
    public interface IStepService
    {
        Task<List<Step>> GetStepsByIdsAsync(List<string>? ids);
        Task<Step?> GetStepByIdAsync(string id);
        Task CreateStepAsync(Step step);
        Task UpdateStepAsync(Step step);
        Task DeleteStepAsync(string id);
    }
}
