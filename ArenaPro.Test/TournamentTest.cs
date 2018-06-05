using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArenaPro.Domain.Test
{
    [TestClass]
    public class TournamentTest
    {
        [TestMethod]
        public void Verify_Tournament_Is_Open()
        {
            var tournament = GetTournament();
            tournament.FinalDate = DateTime.Now.AddDays(30);
            Assert.IsTrue(tournament.IsOpen);
        }
       
        [TestMethod]
        public void Verify_OneDay_Tournament_Is_Open()
        {
            var tournament = GetTournament();
            Assert.IsTrue(tournament.IsOpen);
        }

        [TestMethod]
        public void Verify_OneDay_Tournament_Is_Not_Open()
        {
            var tournament = GetTournament();
            tournament.InitialDate = DateTime.Now.AddDays(1);
            tournament.FinalDate = DateTime.Now.AddDays(1);

            Assert.IsFalse(tournament.IsOpen);
        }


        public Tournament GetTournament()
        {
            var tournament = new Tournament();
            tournament.InitialDate = DateTime.Now;
            tournament.FinalDate = DateTime.Now;
            return tournament;
        }
    }


}
