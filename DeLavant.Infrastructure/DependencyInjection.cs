using DeLavant.Domain.Courses;
using DeLavant.Domain.Steps;
using DeLavant.Domain.Tests;
using DeLavant.Infrastructure.Mongo;
using DeLavant.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace DeLavant.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Чтение блока MongoDb из appsettings.json
            services.Configure<MongoDbSettings>(
                configuration.GetSection("MongoDb"));

            // Контекст MongoDB
            services.AddSingleton<MongoContext>();

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStepRepository, StepRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();


            return services;
        }
    }

}
