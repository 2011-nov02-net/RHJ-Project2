using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public class UserRepo
    {

        private readonly DbContextOptions<Project2Context> _contextOptions;
        public UserRepo(DbContextOptions<Project2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            using var context = new Project2Context(_contextOptions);
            var dbUsers = await context.Customers.ToListAsync();
            var appUsers = DomainDataMapper.GetAllUsers(dbUsers);
            return appUsers;

        }

        public async Task<AppUser> GetOneUser(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbUser = context.Customers.FirstOrDefault(x => x.UserId == id);
            var appUser = DomainDataMapper.GetOneUser(dbUser);
            return appUser;
        }

        public async Task AddOneUser(AppUser user)
        { 
            using var context = new Project2Context(_contextOptions);
            var newUser = DomainDataMapper.AddOneUser(user);
            await context.Customers.AddAsync(newUser);
            await context.SaveChangesAsync();
        }

        






    }
}
