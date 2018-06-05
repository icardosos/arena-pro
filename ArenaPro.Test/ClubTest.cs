using ArenaPro.Domain;
using ArenaPro.Domain.Validation.Club;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArenaPro.Test
{
    [TestClass]
    public class ClubTest
    {
        [TestMethod]
        public void Club_Must_Have_Player()
        {
            var player = new PlayerTradable() { Id = 1 };
            var club = new Club();
            club.AddPlayer(player);

            var validator = new ClubMustHavePlayer(club, player);

            Assert.IsTrue(validator.Validate());
        }

        [TestMethod]
        public void Club_Must_Have_Money()
        {
            var player = new PlayerTradable() { Id = 1 };
            var club = new Club();
            club.AddMoney(1000);

            var validator = new ClubMustHaveMoney(club,1000);
            Assert.IsTrue(validator.Validate());
        }
    }
}
