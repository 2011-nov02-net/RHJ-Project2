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
        private readonly DbContextOptions<Project2Context> _contextOptions;
        public StoreRepo(DbContextOptions<Project2Context> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        //Get all packs in store inventory
        public async Task<IEnumerable<AppStoreItem>> GetAllStoreItems()
        {
            using var context = new Project2Context(_contextOptions);

            var dbStorePacks = await context.StoreInventories.ToListAsync();

            if (dbStorePacks == null)
                return null;

            var appPacks = DomainDataMapper.GetAllStorePacks(dbStorePacks);

            return appPacks;
        }

        //Get a specific pack by id
        public async Task<AppStoreItem> GetStoreItemById(string id)
        {
            using var context = new Project2Context(_contextOptions);

            var dbPack = await context.StoreInventories.Where(x => x.PackId == id).FirstAsync();

            if (dbPack == null)
                return null;

            var appPack = DomainDataMapper.GetStorePackById(dbPack);

            return appPack;
        }

        //Update pack by id
        //add if manager adds more stock
        //sub if user purchases a pack to decrease stock
        public async Task<bool> UpdateStoreItemById(string id, string option, int amount)
        {
            using var context = new Project2Context(_contextOptions);
            var pack = await context.StoreInventories.Where(x => x.PackId == id).FirstAsync();
            if (pack == null)
                return false;

            switch(option)
            {
                case "add":
                    try
                    {
                        pack.PackQty += amount;
                        await context.SaveChangesAsync();
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine("Try to Add to Store Qty Exception: " + e);
                    }
                    break;
                case "sub":
                    try
                    {
                        pack.PackQty -= amount;
                        await context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Try to Subtract to Store Qty Exception: " + e);
                    }
                    break;
                default:
                    Debug.WriteLine("Update Store Qty Error");
                    break;
            }

            return false;
        }
    }
}
