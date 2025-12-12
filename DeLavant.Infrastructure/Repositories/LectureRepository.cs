using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeLavant.Domain.Lectures;
using DeLavant.Infrastructure.Mongo;

namespace DeLavant.Infrastructure.Repositories
{
    public class LectureRepository : BaseMongoRepository<Lecture>, ILectureRepository
    {
        public LectureRepository(MongoContext context)
           : base(context.Lectures) // ← передаём коллекцию
        {
        }
    }
  
}
