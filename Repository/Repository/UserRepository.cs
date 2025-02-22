using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string? role)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    if (role == null) {
                        return await context.Users.ToListAsync();
                    }

                    return await context.Users
                        .Where(a => a.Role == role)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
