using DeLavant.Application.Steps;
using DeLavant.Domain.Courses;
using DeLavant.Domain.Steps;

namespace DeLavant.Application.Courses
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStepService _stepService;

        public CourseService(
        ICourseRepository courseRepository,
        IStepService stepService)
        {
            _courseRepository = courseRepository;
            _stepService = stepService;
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

        public async Task SaveCourseAsync(Course course, List<Step> steps)
        {
            var existing = await _courseRepository.GetCourseByIdAsync(course.Id);

            // Устанавливаем Id шагов в курсе
            course.Elements = steps.Select(s => s.Id).ToList();

            // Удаляем шаги, которых больше нет
            if (existing?.Elements != null)
            {
                var removedSteps = existing.Elements
                    .Where(id => !course.Elements.Contains(id))
                    .ToList();

                foreach (var stepId in removedSteps)
                    await _stepService.DeleteStepAsync(stepId);
            }

            // Сохраняем или обновляем шаги
            foreach (var step in steps)
                await _stepService.SaveStepAsync(step);

            // Сохраняем сам курс
            course.LastUpdatedAt = DateTime.UtcNow;
            if (existing == null)
            {
                course.CreatedAt = DateTime.UtcNow;
                await _courseRepository.CreateCourseAsync(course);
            }
            else
            {
                await _courseRepository.UpdateCourseAsync(course);
            }
        }




        public async Task UpdateCourseAsync(Course updated)
        {
            var existing = await _courseRepository.GetCourseByIdAsync(updated.Id);
            if (existing == null)
                throw new InvalidOperationException("Course not found");

            var removedSteps = existing.Elements?
                .Where(s => !updated.Elements.Contains(s))
                .ToList() ?? new();

            foreach (var stepId in removedSteps)
                await _stepService.DeleteStepAsync(stepId);

            updated.LastUpdatedAt = DateTime.UtcNow;

            await _courseRepository.UpdateCourseAsync(updated);
        }


        public async Task DeleteCourseAsync(string courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null) return;

            if (course.Elements != null)
            {
                foreach (var stepId in course.Elements)
                    await _stepService.DeleteStepAsync(stepId);
            }

            await _courseRepository.DeleteCourseAsync(courseId);
        }
    }

}
