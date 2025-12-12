using DeLavant.Domain.Abstractions;

namespace DeLavant.Domain.Tests
{
    /// <summary>
    /// модель теста
    /// </summary>
    public class Test : ContentItem
    {
       /// <summary>
       /// список вопростов
       /// </summary>
        public List<Question>? Questions { get; set; }
    }

    /// <summary>
    ///  модель вопроса 
    /// </summary>
    public class Question
    {
        public string Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// будет хранить 1 для вида элемента с выбором нескольких
        /// хранить 0 для вида элемента расстановки по порядку
        /// </summary>
        public bool IsMany { get; set; }
        /// <summary>
        /// список ответов
        /// </summary>
        public List<Answer> Answers { get; set; }
    }
    public struct Answer
    {
        public string Id { get; set; }
        /// <summary>
        /// текстовое значение ответа
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// булевое значение используется при выборе одного
        /// выборе нескольких
        /// </summary>
        public bool? IsCorrect {  get; set; }
    }
}
