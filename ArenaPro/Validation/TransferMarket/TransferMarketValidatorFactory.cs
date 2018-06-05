
namespace ArenaPro.Domain.Validation.TransferMarket
{
    public class TransferMarketValidatorFactory
    {
        public static IValidator RangePriceTrade(Domain.TradeOffer offer1, Domain.TradeOffer offer2, decimal tradePercent)
        {
            return new RangePriceTrade(offer1, offer2, tradePercent);
        }
    }
}
