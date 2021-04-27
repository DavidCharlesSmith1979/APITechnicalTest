using System.Threading.Tasks;
using TechnicalTest.Models;

namespace TechnicalTest.Services
{
    public interface IUserService
    {
        Task Create(User user);
    }
}