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

        private readonly Project2Context _context;
        public PackRepo(Project2Context context)
        {
            _context = context;
        }

        // not mapped
        public async Task<IEnumerable<AppPack>> GetAllPacks()
        {
        
            var dbPacks = await _context.Packs.ToListAsync();
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
 
            var dbPack = await _context.Packs.FirstOrDefaultAsync(x => x.PackId == id);
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
