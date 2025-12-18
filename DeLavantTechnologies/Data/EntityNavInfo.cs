namespace DeLavantTechnologies.Data
{
    public class EntityNavInfo
    {
        public string Title { get; set; } = "";
        public string IconClass { get; set; } = "";
        public string? Style { get; set; }
    }
}

namespace DeLavantTechnologies.Data
{
    public static class EntityNavDefinitions
    {
        public static readonly Dictionary<EntityType, EntityNavInfo> EntityNavMap =
            new()
            {
                [EntityType.DestinationCourses] = new()
                {
                    Title = "Мои курсы",
                    IconClass = "collection-fill-icon",
                    Style = "--gradient: var(--primary-red);"
                },
                [EntityType.Courses] = new()
                {
                    Title = "Курсы",
                    IconClass = "collection-fill-icon",
                    Style = "--gradient: var(--primary-red);"
                },
                [EntityType.Users] = new()
                {
                    Title = "Пользователи",
                    IconClass = "users-fill-icon",
                    Style = "--gradient: var(--primary-gray);"
                },
                [EntityType.Positions] = new()
                {
                    Title = "Должности",
                    IconClass = "tools-icon",
                    Style = "--gradient: var(--primary-gray);"
                },
                [EntityType.Applicants] = new()
                {
                    Title = "Соискатели",
                    IconClass = "users-icon",
                    Style = "--gradient: var(--secondary-gray);"
                },
                [EntityType.Projects] = new()
                {
                    Title = "Проекты",
                    IconClass = "collection-icon",
                    Style = "--gradient: var(--primary-gray);"
                },
                [EntityType.Home] = new()
                {
                    Title = "Главная",
                    IconClass = "house-fill-icon",
                    Style = "--gradient: var(--primary-gray);"
                }
            };
    }
}
