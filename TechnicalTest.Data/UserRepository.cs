using System;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTest.Data.Models;

namespace TechnicalTest.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task Create(User user)
        {
            await _userContext.Users.AddAsync(user);
            await _userContext.SaveChangesAsync();
        }

        public async Task<User> Get(string username)
        {
            return _userContext.Users
                       .Where(x => x.Name == username)
                       .FirstOrDefault<User>();
        }
    }
}
