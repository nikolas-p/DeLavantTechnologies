namespace DeLavantTechnologies.Data
{
    public class Permissions
    {
        public const string EditUsers = "edit_users";
        public const string CreateNews = "create_news";
        public const string EditNews = "edit_news";
        public const string DeleteNews = "delete_news";
        public const string EditRoles = "edit_roles";

        public static readonly List<string> All = new()
        {
            EditUsers,
            CreateNews,
            EditNews,
            DeleteNews,
            EditRoles
        };
    }
}
