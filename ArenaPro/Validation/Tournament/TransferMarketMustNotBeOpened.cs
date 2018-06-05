
namespace ArenaPro.Domain.Validation.Tournament
{
    public class TransferMarketMustNotBeOpened : IValidator
    {
        Domain.Tournament _tournament;

        public TransferMarketMustNotBeOpened(Domain.Tournament tournament)
        {
            this._tournament = tournament;
        }

        public string Message
        {
            get { return "Transfer market is already opened."; }
        }

        public bool Validate()
        {
            return this._tournament.CurrentTransferMarket == null;
        }


    }
}
