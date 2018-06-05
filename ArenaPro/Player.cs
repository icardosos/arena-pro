using System;
using System.Collections.Generic;

namespace ArenaPro.Domain
{
    public abstract class Player
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public List<PlayerPosition> Positions { get; set; }
        public DateTime Born { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int OverallRating { get; set; }
        public int Potential { get; set; }
        public string OriginalClub { get; set; }
		public decimal BasePrice {get;set;}
    }
}
