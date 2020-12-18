using Microsoft.EntityFrameworkCore;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.DataAccess.Entities.Repo.Interfaces;

namespace Project2.DataAccess.Entities.Repo
{
    public class StoreRepo : IStoreRepo
    {
        private readonly Project2Context _context;
        public StoreRepo( Project2Context context)
        {
            _context = context;
        }

        //Get all packs in store inventory
        public async Task<IEnumerable<AppStoreItem>> GetAllStoreItems()
        {
             

            var dbStorePacks = await _context.StoreInventories.ToListAsync();

            if (dbStorePacks == null)
                return null;

            var appPacks = DomainDataMapper.GetAllStorePacks(dbStorePacks);

            return appPacks;
        }

        //Get a specific pack by id
        public async Task<AppStoreItem> GetStoreItemById(string id)
        {
          

            var dbPack = await _context.StoreInventories.Where(x => x.PackId == id).FirstAsync();

            if (dbPack == null)
                return null;

            var appPack = DomainDataMapper.GetStorePackById(dbPack);

            return appPack;
        }

        //Update pack by id
        //add if manager adds more stock
        //sub if user purchases a pack to decrease stock
        public async Task<bool> UpdateStoreItemById(string id, int amount)
        {
            
            var pack = await _context.StoreInventories.Where(x => x.PackId == id).FirstAsync();
            if (pack == null)
                return false;

            try
            {
                pack.PackQty = amount;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Try to Update Store Qty Exception: " + e);
            }

            return false;
        }
    }
}
