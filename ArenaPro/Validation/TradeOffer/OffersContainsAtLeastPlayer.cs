
namespace ArenaPro.Domain.Validation.TradeOffer
{
    public class OffersContainsAtLeastPlayer : IValidator
    {
        Domain.TradeOffer _offer1;
        Domain.TradeOffer _offer2;

        public OffersContainsAtLeastPlayer(Domain.TradeOffer offer1, Domain.TradeOffer offer2)
        {
            this._offer1 = offer1;
            this._offer2 = offer2;
        }
        public string Message
        {
            get { return "The Offers must have at least one player."; }
        }

        public bool Validate()
        {
            return this._offer1.Players.Count > 0 || this._offer2.Players.Count > 0;
        }
    }
}
