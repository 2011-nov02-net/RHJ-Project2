using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Project2.DataAccess.Entities.Repo
{
    public class PackRepo// : IPackRepo
    {

        private readonly DbContextOptions<Project2Context> _contextOptions;
        public PackRepo(DbContextOptions<Project2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        // not mapped
        /*public async Task<AppPack> GetAllPacks()
        {
            using var context = new Project2Context(_contextOptions);
            var dbPacks = await context.Packs.ToListAsync();
            if (dbPacks == null) return null;
            var appPacks = dbPacks.Select(x => new AppPack(x.PackId, x.Name, x.Price, x.DateReleased));
            return appPacks;    
        }

        public async Task<AppPack> GetOnePack(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbPack = await context.Packs.FirstOrDefaultAsync(x => x.PackId == id);
            if (dbPack == null) return null;
            var appPack = new AppPack(dbPack.PackId, dbPack.Name, dbPack.Price, dbPack.DateReleased);
            return appPack;
        }*/
    }
}
