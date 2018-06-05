
using System;

namespace ArenaPro.Domain.Validation.TradeOffer
{
    public static class TradeOfferValidatorFactory
    {
        public static IValidator OffersContainsAtLeastPlayer(Domain.TradeOffer offer1, Domain.TradeOffer offer2)
        {
            return new OffersContainsAtLeastPlayer(offer1, offer2);
        }

        public static IValidator OffersContainAtLeastMoney(Domain.TradeOffer offer1, Domain.TradeOffer offer2)
        {
            return new OffersContainAtLeastMoney(offer1, offer2);
        }


        public static IValidator OffersMaxPlayer(Domain.TradeOffer offer1, Domain.TradeOffer offer2, int number)
        {
            return new OffersMaxPlayer(offer1, offer2, number);
        }

        public static IValidator OffersMinPlayer(Domain.TradeOffer offer1, Domain.TradeOffer offer2, int number)
        {
            return new OffersMinPlayer(offer1, offer2, number);
        }

        public static IValidator OffersCannotContainsMoney(Domain.TradeOffer offer1, Domain.TradeOffer offer2)
        {
            return new OffersCannotContainMoney(offer1, offer2);
        }

        internal static IValidator OfferCannotHavePlayer(Domain.TradeOffer offer, PlayerTradable player)
        {
            return new OfferCannotHavePlayer(offer, player);
        }
    }
}
