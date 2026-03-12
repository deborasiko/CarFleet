using CarFleet.Core.Models;

namespace CarFleet.Core.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId);
}
