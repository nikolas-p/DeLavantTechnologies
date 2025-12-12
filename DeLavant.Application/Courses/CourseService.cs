using DeLavant.Domain.Courses;

namespace DeLavant.Application.Courses
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllCoursesAsync();

        }

        public async Task<Course?> GetCourseByIdAsync(string id)
        {
            return await _courseRepository.GetCourseByIdAsync(id);
        }

        public async Task CreateCourseAsync(Course course)
        {
            await _courseRepository.CreateCourseAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await _courseRepository.UpdateCourseAsync(course);
        }

        public async Task DeleteCourseAsync(string id)
        {
            await _courseRepository.DeleteCourseAsync(id);
        }
    }

}
