using Application.Interfaces.IRepo;
using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AppDbContext context;

        public AuthRepo(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await context.Users
                .Include(u=>u.Role)
                .FirstOrDefaultAsync(u=>u.Username == username);
        }

        public async Task UpdateAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
}
