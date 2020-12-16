using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Project2.DataAccess.Entities.Repo.Interfaces;

namespace Project2.DataAccess.Entities.Repo
{
    public class PackRepo : IPackRepo
    {

        private readonly DbContextOptions<Project2Context> _contextOptions;
        public PackRepo(DbContextOptions<Project2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        // not mapped
        public async Task<IEnumerable<AppPack>> GetAllPacks()
        {
            using var context = new Project2Context(_contextOptions);
            var dbPacks = await context.Packs.ToListAsync();
            if (dbPacks == null) return null;
            var appPacks = dbPacks.Select(x => new AppPack
            {
                PackId = x.PackId,
                Name = x.Name,
                Price = x.Price,
                DateReleased = x.DateReleased,
            });
            return appPacks;    
        }

        // not mapped
        public async Task<AppPack> GetOnePack(string id)
        {
            using var context = new Project2Context(_contextOptions);
            var dbPack = await context.Packs.FirstOrDefaultAsync(x => x.PackId == id);
            if (dbPack == null) return null;
            var appPack = new AppPack
            {
                PackId = dbPack.PackId,
                Name = dbPack.Name,
                Price = dbPack.Price,
                DateReleased = dbPack.DateReleased,
            };
            return appPack;
        }
    }
}
