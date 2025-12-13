using DeLavant.Domain.Abstractions;
using DeLavant.Domain.Courses;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DeLavant.Domain.Steps
{
    /// <summary>
    /// модель раздела курса
    /// </summary>
    public class Step : ContentItem
    {
        /// <summary>
        /// хранит ссылки на ресурсы
        /// </summary>
        /// <param name="bool"> тип элемнта 0 для лекции, 1 для теста</param>
        /// <param name="string"> ссылка на элемент в определённой коллекции</param>
        public List<StepElement> Elements { get; set; } = new();


    }

    public class StepElement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public bool IsTest { get; set; }
    }

}
