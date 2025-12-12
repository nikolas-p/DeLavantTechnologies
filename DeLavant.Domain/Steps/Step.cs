using DeLavant.Domain.Abstractions;
using DeLavant.Domain.Courses;

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
        public List<Dictionary<bool, string>> Elements { get; set; } = new();

    }
}
