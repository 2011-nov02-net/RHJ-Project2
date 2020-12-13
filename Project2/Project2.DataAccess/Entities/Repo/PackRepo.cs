using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public class PackRepo : IPackRepo
    {
        public Task<AppPack> GetAllPacks()
        {
            throw new NotImplementedException();
        }

        public Task<AppPack> GetOnePack(string id)
        {
            throw new NotImplementedException();
        }
    }
}
