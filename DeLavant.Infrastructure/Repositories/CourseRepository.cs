using DeLavant.Domain.Courses;
using DeLavant.Infrastructure.Mongo;

namespace DeLavant.Infrastructure.Repositories
{
    public class CourseRepository : BaseMongoRepository<Course>, ICourseRepository
    {
       
        public CourseRepository(MongoContext context)
         : base(context.Courses) { }

        public Task<List<Course>> GetAllCoursesAsync()
         => GetAllAsync();

        public Task<Course?> GetCourseByIdAsync(string id)
            => GetByIdAsync(id);

        public Task CreateCourseAsync(Course course)
            => CreateAsync(course);

        public Task UpdateCourseAsync(Course course)
            => UpdateAsync(course);

        public Task DeleteCourseAsync(string id)
            => DeleteAsync(id);
    }
}

