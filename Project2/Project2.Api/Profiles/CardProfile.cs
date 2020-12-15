using AutoMapper;
using Project2.Api.DTO;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.Profiles
{
    public class CardProfile: Profile
    {
        // each DTO tranformation needs a profile
        public CardProfile()
        { 
            // source -> target

            // CardDTO  <- AppCard <- dbCard
            // Get
            CreateMap<AppCard,CardDTO>();

            // CardDTO -> AppCard -> dbCard
            // Post
            CreateMap<CardDTO,AppCard>();
        }
    }
}
