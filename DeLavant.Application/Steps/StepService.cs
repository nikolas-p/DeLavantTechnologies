using DeLavant.Application.Tests;
using DeLavant.Domain.Lectures;
using DeLavant.Domain.Steps;

namespace DeLavant.Application.Steps
{
    public class StepService : IStepService
    {
        private readonly IStepRepository _stepRepository;
        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;
        private readonly ILectureRepository _lectureRepository;

        // Конструктор с DI
        public StepService(
        IStepRepository stepRepository,
        ITestService testService,
        IQuestionService questionService,
        ILectureRepository? lectureRepository = null)
        {
            _stepRepository = stepRepository;
            _testService = testService;
            _questionService = questionService;
            //_lectureRepository = lectureRepository;
        }

        public async Task<List<Step>> GetStepsByIdsAsync(List<string>? ids)
        {
            if (ids == null || ids.Count == 0) return new List<Step>();
            return await _stepRepository.GetStepsByIdsAsync(ids);
        }

        public async Task<Step?> GetStepByIdAsync(string id)
        {
            return await _stepRepository.GetStepByIdAsync(id);
        }

        public async Task CreateStepAsync(Step step)
        {
            await _stepRepository.CreateStepAsync(step);
        }
        public async Task SaveStepAsync(Step step)
        {
            if (step == null)
                throw new ArgumentNullException(nameof(step));

            var existing = string.IsNullOrEmpty(step.Id)
                ? null
                : await _stepRepository.GetStepByIdAsync(step.Id);

            step.Elements ??= new List<StepElement>();

            // Удаляем элементы, которых больше нет
            if (existing?.Elements != null)
            {
                var removedElements = existing.Elements
                    .Where(old => !step.Elements.Any(n => n.Id == old.Id))
                    .ToList();

                foreach (var el in removedElements)
                    await DeleteElementAsync(el);
            }

            // Сохраняем или обновляем элементы
            foreach (var el in step.Elements)
            {
                if (el.IsTest && el.Id != null)
                {
                    var test = await _testService.GetTestByIdAsync(el.Id);
                    if (test != null)
                    {
                        await _testService.SaveTestAsync(test);
                    }
                }
                //else if (!el.IsTest && el.Lecture != null)
                //    await _lectureRepository.SaveAsync(el.Lecture);
            }

            // Сохраняем или обновляем сам Step
            if (existing == null)
                await _stepRepository.CreateStepAsync(step);
            else
                await _stepRepository.UpdateStepAsync(step);
        }

        // Вспомогательный метод для удаления элементов
        private async Task DeleteElementAsync(StepElement el)
        {
            if (el.IsTest)
            {
                var test = await _testService.GetTestByIdAsync(el.Id);
                if (test?.Questions != null)
                {
                    foreach (var qId in test.Questions)
                        await _questionService.DeleteQuestionAsync(qId);
                }
                await _testService.DeleteTestAsync(el.Id);
            }
            //else if (el.Lecture != null)
            //{
            //    await _lectureRepository.DeleteAsync(el.Lecture.Id);
            //}
        }


        public async Task UpdateStepAsync(Step updatedStep)
        {
            var existing = await _stepRepository.GetStepByIdAsync(updatedStep.Id);
            if (existing == null)
                throw new InvalidOperationException("Step not found");

            // 1️⃣ Найти удалённые элементы
            var removedElements = existing.Elements
                .Where(old => !updatedStep.Elements.Any(n => n.Id == old.Id))
                .ToList();

            // 2️⃣ КАСКАД УДАЛЕНИЯ
            foreach (var el in removedElements)
            {
                if (el.IsTest)
                {
                    var test = await _testService.GetTestByIdAsync(el.Id);
                    if (test?.Questions != null)
                    {
                        foreach (var qId in test.Questions)
                            await _questionService.DeleteQuestionAsync(qId);
                    }

                    await _testService.DeleteTestAsync(el.Id);
                }
                else
                {
                    //await _lectureRepository.DeleteAsync(el.Id);
                }
            }

            // 3️⃣ Обновляем сам Step
            await _stepRepository.UpdateStepAsync(updatedStep);
        }


        public async Task DeleteStepAsync(string stepId)
        {
            var step = await _stepRepository.GetStepByIdAsync(stepId);
            if (step == null) return;

            foreach (var el in step.Elements)
            {
                if (el.IsTest)
                {
                    var test = await _testService.GetTestByIdAsync(el.Id);
                    if (test?.Questions != null)
                    {
                        foreach (var qId in test.Questions)
                            await _questionService.DeleteQuestionAsync(qId);
                    }

                    await _testService.DeleteTestAsync(el.Id);
                }
                else
                {
                    //await _lectureRepository.D(el.Id);
                }
            }

            await _stepRepository.DeleteStepAsync(stepId);
        }


    
}

}
