using ArenaPro.Infrastructure.UnityIoC;
using ArenaPro.SharedKernel.Events;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ArenaPro.Domain.Test
{
    [TestClass]
    public class TransferMarketTest
    {
        public TransferMarketTest()
        {
            var container = new UnityContainer();
            DependencyRegister.Register(container);
            EventTrigger.ContainerResolver = new UnityResolverHelper(container);
        }

        public IEventHandler<DomainNotification> Notifications
        {
            get
            {
                return EventTrigger.ContainerResolver.GetService<IEventHandler<DomainNotification>>();
            }
        }


        [TestMethod]
        public void Transfer_Player_To_Another_Club()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var messi = barcelona.GetPlayer(1);
            var aguero = manchester.GetPlayer(2);

            var buyerOffer = new TradeOffer(manchester);
            buyerOffer.AddMoney(5000);

            var sellerOffer = new TradeOffer(barcelona);
            sellerOffer.AddPlayer(messi);

            market.Trade(buyerOffer, sellerOffer);



            Assert.AreEqual(true, manchester.HasPlayer(messi) && !barcelona.HasPlayer(messi) && manchester.Money == 0 && barcelona.Money == 10000);
        }

        [TestMethod]
        public void TradeOffer_Player_And_Money()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var messi = barcelona.GetPlayer(1);
            var aguero = manchester.GetPlayer(2);

            var buyerOffer = new TradeOffer(manchester);
            buyerOffer.AddMoney(2500);
            buyerOffer.AddPlayer(aguero);

            var sellerOffer = new TradeOffer(barcelona);
            sellerOffer.AddPlayer(messi);

            market.Trade(buyerOffer, sellerOffer);



            Assert.AreEqual(true, manchester.HasPlayer(messi) &&
                                    !barcelona.HasPlayer(messi) &&
                                    manchester.Money == 2500 &&
                                    barcelona.Money == 7500 &&
                                    !manchester.HasPlayer(aguero) &&
                                    barcelona.HasPlayer(aguero));
        }

        [TestMethod]
        public void Open_TransferMarket_With_TounamentClosed()
        {
            var tournament = GetTournament();
            tournament.InitialDate = DateTime.Now.AddDays(-2);
            tournament.FinalDate = DateTime.Now.AddDays(-2);
            var market = tournament.OpenTransferMarket(DateTime.Now, DateTime.Now, 100000, 10, 15);
            Assert.AreEqual(true, market == null && tournament.TransferMarket.Count == 0);

        }

        [TestMethod]
        public void Try_Open_TransferMarket_Already_Opened()
        {
            var tournament = GetTournament();
            tournament.TransferMarket.Add(new TransferMarket() { InitalDate = DateTime.Now, FinalDate = DateTime.Now });
            var market = tournament.OpenTransferMarket(DateTime.Now, DateTime.Now.AddDays(3), 100000, 10, 15);
            Assert.AreEqual(true, market == null && tournament.TransferMarket.Count == 1);

        }

        [TestMethod]
        public void Get_Current_TransferMarket()
        {
            var tournament = GetTournament();
            tournament.TransferMarket.Add(new TransferMarket() { Id = 1, InitalDate = DateTime.Now.AddDays(-10), FinalDate = DateTime.Now.AddDays(-7) });
            tournament.TransferMarket.Add(new TransferMarket() { Id = 2, InitalDate = DateTime.Now, FinalDate = DateTime.Now });
            var market = tournament.CurrentTransferMarket;
            Assert.AreEqual(2, market.Id);
        }

        [TestMethod]
        public void Try_Get_Current_TransferMarket()
        {
            var tournament = GetTournament();
            tournament.TransferMarket.Add(new TransferMarket() { Id = 1, InitalDate = DateTime.Now.AddDays(-10), FinalDate = DateTime.Now.AddDays(-7) });
            tournament.TransferMarket.Add(new TransferMarket() { Id = 2, InitalDate = DateTime.Now.AddDays(-2), FinalDate = DateTime.Now.AddDays(-1) });
            var market = tournament.CurrentTransferMarket;
            Assert.IsNull(market);
        }


        [TestMethod]
        public void Open_TransferMarket()
        {
            var tournament = GetTournament();
            var market = tournament.OpenTransferMarket(DateTime.Now, DateTime.Now.AddDays(3), 100000, 10, 15);
            Assert.AreEqual(true, market != null && tournament.TransferMarket.Count == 1);
        }

        [TestMethod]
        public void Open_OneDay_TransferMarket()
        {
            var tournament = GetTournament();
            var market = tournament.OpenTransferMarket(DateTime.Now, DateTime.Now, 100000, 10, 15);
            Assert.AreEqual(true, market != null && tournament.TransferMarket.Count == 1);

        }

        public Tournament GetTournament()
        {
            var tournament = new Tournament();
            tournament.InitialDate = DateTime.Now;
            tournament.FinalDate = DateTime.Now;
            return tournament;
        }

        private static void SetUpMock(out TransferMarket market, out Club manchester, out Club barcelona)
        {
            market = new TransferMarket();
            market.TradePercent = 20;
            market.MaxPlayerTrade = 2;
            market.MinPlayerTrade = 1;

            manchester = new Club() { Id = 1, Name = "Manchester United", Money = 5000 };
            barcelona = new Club() { Id = 2, Name = "Barcelona", Money = 5000 };

            var messi = new PlayerTradable() { Id = 1, Price = 5000, Name = "Lionel Messi" };
            var neymar = new PlayerTradable() { Id = 3, Price = 4000, Name = "Neymar Jr" };
            var suarez = new PlayerTradable() { Id = 5, Price = 4000, Name = "Suarez" };
            barcelona.AddPlayer(messi);
            barcelona.AddPlayer(neymar);
            barcelona.AddPlayer(suarez);


            var aguero = new PlayerTradable() { Id = 2, Price = 2200, Name = "Aguero" };
            var toure = new PlayerTradable() { Id = 4, Price = 1800, Name = "Toure" };
            manchester.AddPlayer(aguero);
            manchester.AddPlayer(toure);
        }

    }
}
