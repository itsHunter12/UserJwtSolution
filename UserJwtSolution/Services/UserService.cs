using Microsoft.EntityFrameworkCore;
using UserJwtSolution.Models;
using System.Collections.Generic;

namespace UserJwtSolution.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUser(User user)
        {
            if (await _context.As_Users.AnyAsync(u => u.Email == user.Email))
                return false; // User with the same email already exists

            _context.As_Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            return await _context.As_Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.As_Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.As_Users.FindAsync(id);
        }
    }
}
