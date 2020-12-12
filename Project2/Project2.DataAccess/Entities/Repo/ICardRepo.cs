using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public interface ICardRepo
    {
        Task<IEnumerable<AppCard>> GetAllCards();
        Task<AppCard> GetOneCard(string id);
        Task AddOneCard(AppCard card);

    }
}
