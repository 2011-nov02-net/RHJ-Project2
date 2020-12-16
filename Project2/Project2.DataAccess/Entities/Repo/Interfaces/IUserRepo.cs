﻿using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DataAccess.Entities.Repo
{
    public interface IUserRepo
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetOneUser(string id);
        Task<AppUser> AddOneUser(AppUser user);
        Task<IEnumerable<AppCard>> GetAllCardsOfOneUser(string id);
        Task<AppCard> GetOneCardOfOneUser(string id, string cardId);

        Task<AppCard> AddOneCardToOneUser(string id, AppCard card);
        Task<string> DeleteOneCardOfOneUser(string id, string cardId);


    }
}