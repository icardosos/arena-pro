
namespace ArenaPro.Domain.Validation.Tournament
{
    public static class TournamentValidatorFactory
    {
        public static IValidator TournamentMustBeOpen(Domain.Tournament tournament)
        {
            return new TournamentMustBeOpen(tournament);
        }

        public static IValidator TransferMarketMustNotBeOpened(Domain.Tournament tournament)
        {
            return new TransferMarketMustNotBeOpened(tournament);
        }
    }
}
