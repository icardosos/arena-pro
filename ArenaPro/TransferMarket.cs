using System;
using System.Collections.Generic;
using ArenaPro.Domain.Validation;
using ArenaPro.Domain.Validation.TransferMarket;
using ArenaPro.SharedKernel.Events;
using ArenaPro.Domain.Validation.TradeOffer;

namespace ArenaPro.Domain
{
    public class TransferMarket 
    {
        public int Id { get; set; }
        public decimal MaxPlayerPrice { get; set; }
        public int StolenPercent { get; set; }
        public int TradePercent { get; set; }
        public int MaxPlayerTrade { get; set; }
        public int MinPlayerTrade { get; set; }
        public DateTime InitalDate { get; set; }
        public DateTime FinalDate { get; set; }
        public Tournament Tournament { get; set; }

        public bool IsOpen
        {
            get
            {
                return DateTime.Now.Date >= this.InitalDate.Date && DateTime.Now.Date <= this.FinalDate.Date;
            }
        }

        public void Trade(TradeOffer offer1, TradeOffer offer2)
        {
            var validators
                = new List<IValidator> {
                    TradeOfferValidatorFactory.OffersContainsAtLeastPlayer(offer1,offer2), 
                    TradeOfferValidatorFactory.OffersContainAtLeastMoney(offer1,offer2), 
                    TransferMarketValidatorFactory.RangePriceTrade(offer1, offer2, this.TradePercent) };

            if (!ValidationHelper.ValidateProcess(validators))
                return;

            this.ProcessTrade(offer1, offer2);
        }

        public void ExchangePlayer(TradeOffer offer1, TradeOffer offer2)
        {
            var validators
                  = new List<IValidator> { 
                                TradeOfferValidatorFactory.OffersMinPlayer(offer1,offer2,this.MinPlayerTrade),
                                TradeOfferValidatorFactory.OffersMaxPlayer(offer1,offer2, this.MaxPlayerTrade),
                                TradeOfferValidatorFactory.OffersCannotContainsMoney(offer1,offer2)
                    };

            if (!ValidationHelper.ValidateProcess(validators))
                return;

            this.ProcessTrade(offer1, offer2);
        }

    
        void ProcessTrade(TradeOffer offer1, TradeOffer offer2)
        {

            foreach (var item in offer1.Players)
            {
                offer2.Club.AddPlayer(item);
                offer1.Club.RemovePlayer(item);
            }

            foreach (var item in offer2.Players)
            {
                offer1.Club.AddPlayer(item);
                offer2.Club.RemovePlayer(item);
            }


            offer2.Club.RemoveMoney(offer2.Money);
            offer1.Club.AddMoney(offer2.Money);

            offer1.Club.RemoveMoney(offer1.Money);
            offer2.Club.AddMoney(offer1.Money);
        }

       


    }
}
