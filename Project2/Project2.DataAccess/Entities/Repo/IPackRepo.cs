using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public interface IPackRepo
    {
        Task<IEnumerable<AppPack>> GetAllPacks();
        Task<AppPack> GetOnePack(string id);
    }
}
