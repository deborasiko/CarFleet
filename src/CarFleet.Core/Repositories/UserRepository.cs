using Microsoft.EntityFrameworkCore;
using CarFleet.Core.Models;
using CarFleet.Core.Data;

namespace CarFleet.Core.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(FleetDbContext context) : base(context) { }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await DbSet
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await DbSet
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId)
    {
        return await DbSet
            .Where(u => u.RoleId == roleId)
            .Include(u => u.Role)
            .ToListAsync();
    }
}
