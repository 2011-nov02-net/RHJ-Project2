using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppCard
    {
        public string CardId { get; set; }
        public AppPack Pack { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Rarity { get; set; }
        public double Value { get; set; }

        public double Rating { get; set; }
        public int NumOfRatings { get; set; }
        public string? Image { get; set; }

        //currently not tracking the list of ratings
        //private List<double> _ratings;
        private readonly double _rarityWeight = 2;
        private readonly double _ratingWeight = 1;

        public AppCard(double rating, int numOfRatings, int rarity)
        {
            Rating = rating;
            NumOfRatings = numOfRatings;
            Rarity = rarity;
            UpdateValue();
        }

        /// <summary>
        /// adds a rating for the card and calculates a new average rating
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rating"></param>
        /// <returns>new aggregate rating</returns>
        public void NewRating(AppUser user, double rating)
        {
            //TODO: prevent duplicate rating from the same user
            if (user.Inventory.Exists(x => x.CardId == this.CardId) && NumOfRatings != 0)
            {
                //calculate average rating
                double sum = Rating * NumOfRatings;
                ++NumOfRatings;
                sum += rating;
                Rating = sum / NumOfRatings;
                UpdateValue();
            }
            else if(user.Inventory.Exists(x => x.CardId == this.CardId) && NumOfRatings == 0)
            {
                Rating = rating;
                NumOfRatings++;
                UpdateValue();
            }
        }

        /// <summary>
        /// calculate value during construction and when rating changes
        /// </summary>
        public void UpdateValue()
        {
            if (NumOfRatings != 0)
                Value = Rarity * _rarityWeight + (Rating * _ratingWeight) - 2;
            else
                Value = Rarity * _rarityWeight;
        }
    }
}
