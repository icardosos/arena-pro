
namespace ArenaPro.Domain.Validation.TradeOffer
{
    public class OfferCannotHavePlayer : IValidator
    {
        Domain.TradeOffer _offer { get; set; }
        PlayerTradable _player { get; set; }

        public OfferCannotHavePlayer(Domain.TradeOffer offer, Domain.PlayerTradable player)
        {
            this._offer = offer;
            this._player = player;
        }

        public string Message
        {
            get { return "The Offer already have this player."; }
        }

        public bool Validate()
        {
            return !this._offer.Players.Contains(this._player);
        }




    }
}
