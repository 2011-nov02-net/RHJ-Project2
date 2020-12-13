using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public interface IUserRepo
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetOneUser(string id);
        Task AddOneUser(AppUser user);
    }
}
