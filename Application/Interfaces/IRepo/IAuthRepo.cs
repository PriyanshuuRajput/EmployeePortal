using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepo
{
    public interface IAuthRepo
    {
        Task<User?> GetByUsernameAsync(string username);
        Task UpdateAsync(User user);
    }
}
