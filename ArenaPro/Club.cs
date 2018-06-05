using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ArenaPro.Domain
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Tournament Tournament { get; set; }
        public User User { get; set; }
        public decimal Money { get; set; }

        IList<PlayerTradable> _players;
        IList<PlayerTradable> Players
        {
            get
            {
                return _players ?? (_players = new List<PlayerTradable>());
            }
            set { _players = value; }
        }

        IList<TradeOffer> _transferOffers;
        public IList<TradeOffer> TransferOffers
        {
            get
            {
                return _transferOffers ?? (_transferOffers = new List<TradeOffer>());
            }
            set { _transferOffers = value; }
        }

        public PlayerTradable GetPlayer(int playerId)
        {
            return this.Players.FirstOrDefault(x => x.Id == playerId);
        }

        public bool HasPlayer(PlayerTradable player)
        {
            return this.Players.Any(x => x.Id == player.Id);
        }

        public void AddPlayer(PlayerTradable player)
        {
            this.Players.Add(player);
        }

        public void RemovePlayer(PlayerTradable player)
        {
            this.Players.Remove(this.Players.FirstOrDefault(x => x.Id == player.Id));
        }

        public void AddMoney(decimal qtd)
        {
            this.Money += qtd;
        }

        public void RemoveMoney(decimal qtd)
        {
            this.Money -= qtd;
        }


    }
}