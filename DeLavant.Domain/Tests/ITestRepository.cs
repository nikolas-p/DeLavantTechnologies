using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeLavant.Domain.Lectures;

namespace DeLavant.Domain.Tests
{
    public interface ITestRepository
    {
        Task<List<Test>> GetTestsByIdsAsync(List<string> TestIds);
        Task<Test?> GetTestByIdAsync(string id);
        Task CreateTestAsync(Test test);
        Task UpdateTestAsync(Test test);
        Task DeleteTestAsync(string id);
    }
    public interface IQuestionRepository
    {
        Task<List<Question>> GetQuestionsByIdsAsync(List<string> QuestionIds);
        Task<Question?> GetQuestionByIdAsync(string id);
        Task CreateQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(string id);
    }

}
