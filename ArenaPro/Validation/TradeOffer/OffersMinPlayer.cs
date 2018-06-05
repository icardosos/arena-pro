
namespace ArenaPro.Domain.Validation.TradeOffer
{
    public class OffersMinPlayer : IValidator
    {
        Domain.TradeOffer _offer1 { get; set; }
        Domain.TradeOffer _offer2 { get; set; }
        int _number { get; set; }


        public OffersMinPlayer(Domain.TradeOffer offer1, Domain.TradeOffer offer2, int number)
        {
            this._offer1 = offer1;
            this._offer2 = offer2;
            this._number = number;
        }

        public string Message
        {
            get { return "The offers must reach the minimal number of player."; }
        }

        public bool Validate()
        {
            return this._offer1.Players.Count >= this._number && this._offer2.Players.Count >= this._number;
        }



    }
}
