using System.Security.Claims;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace DeLavantTechnologies.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
       
        public string LastName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";

        public string? LastLoginTime { get; set; } = "";

        /// <summary>
        /// Должности: "Администратор", "Редактор", "Оператор"
        /// </summary>
        //public List<string> Positions { get; set; } = new();

        /// <summary>
        /// Права (дополнительные Claims)
        /// edit_users, create_news, delete_news, edit_roles, etc.
        /// </summary>
        //public List<string> Permissions { get; set; } = new();

        
    }

}
