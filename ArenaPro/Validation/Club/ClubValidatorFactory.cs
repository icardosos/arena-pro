namespace ArenaPro.Domain.Validation.Club
{
    public class ClubValidatorFactory
    {
        public static IValidator ClubMustHaveMoney(Domain.Club club, decimal valueNeeded)
        {
            return new ClubMustHaveMoney(club, valueNeeded);
        }

        public static IValidator ClubMustHavePlayer(Domain.Club club, PlayerTradable player)
        {
            return new ClubMustHavePlayer(club, player);
        }
    }
}
