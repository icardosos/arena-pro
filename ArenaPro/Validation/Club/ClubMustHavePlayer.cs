using System;

namespace ArenaPro.Domain.Validation.Club
{
    public class ClubMustHavePlayer : IValidator
    {
        Domain.Club _club;
        PlayerTradable _player;

        public ClubMustHavePlayer(Domain.Club club, PlayerTradable player)
        {
            this._club = club;
            this._player = player;
        }
        public string Message
        {
            get { return "Club does not have this player."; }
        }

        public bool Validate()
        {
            return this._club.HasPlayer(this._player);
        }
    }
}
