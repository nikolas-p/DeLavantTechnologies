using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeLavant.Domain.Courses;
using DeLavant.Domain.Steps;

namespace DeLavant.Application.Courses
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(string id);
        Task CreateCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(string id);
        Task SaveCourseAsync(Course course, List<Step> steps);
    }
}
