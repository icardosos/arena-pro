
namespace ArenaPro.Domain.Validation.TradeOffer
{
    public class OffersCannotContainMoney : IValidator
    {

       Domain.TradeOffer _offer1;
       Domain.TradeOffer _offer2;

       public OffersCannotContainMoney(Domain.TradeOffer offer1, Domain.TradeOffer offer2)
        {
            this._offer1 = offer1;
            this._offer2 = offer2;
        }
        public string Message
        {
            get { return "The offers can not contains money."; }
        }

        public bool Validate()
        {
            return this._offer1.Money == 0 && this._offer2.Money == 0;
        }
    }

}
