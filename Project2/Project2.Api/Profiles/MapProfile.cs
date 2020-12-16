using AutoMapper;
using Project2.Api.DTO;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.Profiles
{
    public class MapProfile: Profile
    {
        // each DTO tranformation needs a profile
        public MapProfile()
        {
            // user
            // source -> target
            // Get
            CreateMap<AppUser,UserReadDTO>();
            // Post
            CreateMap<UserCreateDTO, AppUser>();
             

            // card
            CreateMap<AppCard,CardReadDTO>();



            // pack


            //
        }
    }
}
