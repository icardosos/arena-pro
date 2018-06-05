using System;

namespace ArenaPro.Domain.Validation.Tournament
{
    public class TournamentMustBeOpen : IValidator
    {
        Domain.Tournament _tournament;

        public TournamentMustBeOpen(Domain.Tournament tournament)
        {
            this._tournament = tournament;    
        }
        public string Message
        {
            get { return "Tournament is not open."; }
        }

        public bool Validate()
        {
            return this._tournament.IsOpen;
        }

      
    }
}
