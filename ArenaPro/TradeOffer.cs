using ArenaPro.Domain.Validation;
using ArenaPro.Domain.Validation.Club;
using ArenaPro.Domain.Validation.TradeOffer;
using ArenaPro.SharedKernel.Events;
using System.Collections.Generic;

namespace ArenaPro.Domain
{
    public class TradeOffer
    {
        public Club Club;
        public decimal Money { get; private set; }

        IList<PlayerTradable> _players;
        public IList<PlayerTradable> Players
        {
            get
            {
                return _players ?? (_players = new List<PlayerTradable>());
            }
            set
            { _players = value; }
        }

        public TradeOffer(Club club)
        {
            this.Club = club;
        }

        public void AddMoney(decimal value)
        {
            var offerValue = this.Money + value;

            var validators
              = new List<IValidator> {
                    ClubValidatorFactory.ClubMustHaveMoney(this.Club,offerValue)
                };

            if (!ValidationHelper.ValidateProcess(validators))
                return;

            this.Money += value;
        }

        public void AddPlayer(PlayerTradable player)
        {
            var validators
             = new List<IValidator> {
                    TradeOfferValidatorFactory.OfferCannotHavePlayer(this,player),
                 ClubValidatorFactory.ClubMustHavePlayer(this.Club,player)
               };

            if (!ValidationHelper.ValidateProcess(validators))
                return;

            this.Players.Add(player);
        }
    }
}