using DeLavant.Domain.Abstractions;

namespace DeLavant.Domain.Lectures
{
    /// <summary>
    /// модель лекции
    /// </summary>
    public class Lecture : ContentItem
    {
        /// <summary>
        /// ссылки на объет в коллекции медиафайлов
        /// </summary>
         public List<string>? MediaFiles { get; set; }
    }
}
