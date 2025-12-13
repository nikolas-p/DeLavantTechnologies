using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeLavant.Domain.Courses;
using DeLavant.Domain.Lectures;
using DeLavant.Domain.Steps;
using DeLavant.Domain.Tests;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using static System.Net.Mime.MediaTypeNames;

namespace DeLavant.Infrastructure.Mongo
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }

        public MongoContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            Database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Course> Courses => Database.GetCollection<Course>("Courses");
        public IMongoCollection<Step> Steps => Database.GetCollection<Step>("Steps");
        public IMongoCollection<Test> Tests => Database.GetCollection<Test>("Tests");
        public IMongoCollection<Question> Questions => Database.GetCollection<Question>("Question");
        public IMongoCollection<Lecture> Lectures => Database.GetCollection<Lecture>("Lectures");
        //public IMongoCollection<UserAnswer> UserAnswers => Database.GetCollection<UserAnswer>("UserAnswers");
    }
}
