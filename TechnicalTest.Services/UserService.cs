using System;
using System.Threading.Tasks;
using TechnicalTest.Data;

namespace TechnicalTest.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(Models.User user)
        {
            var dtoUser = new Data.Models.User();

            dtoUser.Id = Guid.NewGuid();
            dtoUser.Name = user.Name;

            await _userRepository.Create(dtoUser);
        }
    }
}
