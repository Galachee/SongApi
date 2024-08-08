using Microsoft.EntityFrameworkCore;
using SongApi.Data;
using SongApi.Models;
using SongApi.Repositories.Contracts;

namespace SongApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
        => _context = context;


    public async Task Save(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _context.Users.Include(x=>x.Roles).FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }
}