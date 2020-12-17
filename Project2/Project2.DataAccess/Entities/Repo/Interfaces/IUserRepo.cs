using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo.Interfaces
{
    public interface IUserRepo
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetOneUser(string id);
        Task AddOneUser(AppUser user);
        Task<IEnumerable<AppCard>> GetAllCardsOfOneUser(string id);
        Task<AppCard> GetOneCardOfOneUser(string id, string cardId);
        Task AddOneCardToOneUser(string id, AppCard card);
        Task DeleteOneCardOfOneUser(string id, string cardId);


    }
}
