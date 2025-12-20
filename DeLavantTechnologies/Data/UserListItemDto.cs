//using Microsoft.AspNetCore.Identity;
//using MongoDB.Driver;

//namespace DeLavantTechnologies.Data
//{
//    public class UserListItemDto
//    {
//        public Guid Id { get; set; }

//        public string LastName { get; set; } = "";
//        public string FirstName { get; set; } = "";
//        public string MiddleName { get; set; } = "";

//        public string Email { get; set; } = "";

//        public List<string> Roles { get; set; } = new();
//        public List<string> Positions { get; set; } = new();
//    }

//    public interface IUserDataService
//    {
//        Task<List<UserListItemDto>> GetUsersAsync(int skip, int take);
//        Task<UserEditDto?> GetUserAsync(Guid id);
//        Task UpdateUserAsync(UserEditDto dto);
//        Task DeleteUserAsync(Guid id);
//    }

//    public class UserDataService : IUserDataService
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<ApplicationRole> _roleManager;
//        private readonly IMongoCollection<Position> _positions;

//        public UserDataService(
//            UserManager<ApplicationUser> userManager,
//            RoleManager<ApplicationRole> roleManager,
//            IMongoDatabase database)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _positions = database.GetCollection<Position>("Positions");
//        }

//        public async Task<List<UserListItemDto>> GetUsersAsync(int skip, int take)
//        {
//            var users = _userManager.Users
//                .Skip(skip)
//                .Take(take)
//                .ToList();

//            var roles = _roleManager.Roles.ToList();
//            var positions = _positions.AsQueryable().ToList();

//            return users.Select(u => new UserListItemDto
//            {
//                Id = u.Id,
//                LastName = u.LastName,
//                FirstName = u.FirstName,
//                MiddleName = u.MiddleName,
//                Email = u.Email ?? "",

//                Roles = roles
//                    .Where(r => u.Roles.Contains(r.Id))
//                    .Select(r => r.Name!)
//                    .ToList(),

//                Positions = positions
//                    .Where(p => u.Positions.Contains(p.Id))
//                    .Select(p => p.Name)
//                    .ToList()
//            }).ToList();
//        }
//    }
//}
