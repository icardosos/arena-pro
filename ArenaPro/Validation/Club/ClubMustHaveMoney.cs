
namespace ArenaPro.Domain.Validation.Club
{
    public class ClubMustHaveMoney : IValidator
    {
        Domain.Club _club;
        decimal _valueNeeded;


        public ClubMustHaveMoney(Domain.Club club,decimal valueNeeded)
        {
            this._club = club;
            this._valueNeeded = valueNeeded;

        }
        public string Message
        {
            get { return "The club does not have enough money"; }
        }

        public bool Validate()
        {
            return this._club.Money >= this._valueNeeded;
        }


    }
}
