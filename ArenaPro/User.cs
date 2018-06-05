using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ArenaPro.Domain
{
    public class User
    {
        public int Id { get; set; }
        public int Name { get; set; }

        IList<Club> _clubs;
        public IList<Club> Clubs
        {
            get
            {
                if (_clubs == null)
                    _clubs = new List<Club>();
                return _clubs;
            }
            set
            {
                _clubs = value;
            }
        }

        public Club GetClubByTournament(int tournamentId)
        {
            return Clubs.SingleOrDefault(x => x.Tournament.Id == tournamentId);
        }

        public IEnumerable<Tournament> GetAllTournament()
        {
            foreach (var item in this.Clubs)
            {
                yield return item.Tournament;
            }
        }

    }
}
