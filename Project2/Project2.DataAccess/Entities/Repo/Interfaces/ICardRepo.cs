using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo.Interfaces
{
    public interface ICardRepo
    {
        Task<IEnumerable<AppCard>> GetAllCards();
        Task<AppCard> GetOneCard(string id);
        Task<AppCard> AddOneCard(AppCard card);

    }
}
