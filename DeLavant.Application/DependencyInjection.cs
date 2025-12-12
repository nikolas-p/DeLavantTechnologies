using DeLavant.Application.Courses;
using DeLavant.Application.Lectures;
using DeLavant.Application.MediaFiles;
using DeLavant.Application.Steps;
using DeLavant.Application.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace DeLavant.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStepService, StepService>();


            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<IMediaFileService, MediaFileService>();
            services.AddScoped<ITestService, TestService>();

            return services;
        }
    }
}
