using System.Linq;

namespace ArenaPro.Domain.Validation.TransferMarket
{
    public class RangePriceTrade : IValidator
    {
        Domain.TradeOffer _buyerOffer;
        Domain.TradeOffer _sellerOffer;
        decimal _tradePercent;
        public string Message
        {
            get
            {
                return string.Format("Operation is not allowed. Buyer's offer is above or bellow {0} %.", this._tradePercent);
            }            
        }
        public RangePriceTrade(Domain.TradeOffer buyerOffer, Domain.TradeOffer sellerOffer, decimal tradePercent)
        {
            this._buyerOffer = buyerOffer;
            this._sellerOffer = sellerOffer;
            this._tradePercent = tradePercent;
        }
        public bool Validate()
        {
            decimal buyerOfferValue = this._buyerOffer.Money;
            this._buyerOffer.Players.ToList().ForEach(x => buyerOfferValue += x.Price);

            decimal sellerOfferValue = this._sellerOffer.Money;
            this._sellerOffer.Players.ToList().ForEach(x => sellerOfferValue += x.Price);

            decimal rangePrice = sellerOfferValue * this._tradePercent / 100;
            decimal minPrice = sellerOfferValue - rangePrice;
            decimal maxPrice = sellerOfferValue + rangePrice;

            if (buyerOfferValue < minPrice || buyerOfferValue > maxPrice)
                return false;


            return true;
        }

        
    }
}
