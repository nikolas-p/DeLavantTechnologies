using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeLavant.Domain.Tests;

namespace DeLavant.Application.Tests
{
    public interface ITestService
    {
        Task<Test?> GetTestByIdAsync(string id);
        Task CreateTestAsync(Test test);
        Task UpdateTestAsyc(Test test);
        Task DeleteTestAsync(string id);
    }

    public interface IQuestionService
    {
        Task <List<Question>> GetQuestionsByIdsAsync(List<string> ids);
        Task <Question?> GetQuestionByIdAsync(string id);
        Task CreateQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(string id);
        
    }

}
