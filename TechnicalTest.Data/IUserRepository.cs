using System.Threading.Tasks;
using TechnicalTest.Data.Models;

namespace TechnicalTest.Data
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task<User> Get(string username);
    }
}