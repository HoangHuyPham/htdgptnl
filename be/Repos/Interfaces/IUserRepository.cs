using be.Models;
using be.Repos.Interfaces;

namespace be.Services.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByUserName(string username);
    }
}