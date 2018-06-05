using ArenaPro.Domain;
using ArenaPro.Domain.Validation.TradeOffer;
using ArenaPro.Domain.Validation.TransferMarket;
using ArenaPro.Infrastructure.UnityIoC;
using ArenaPro.SharedKernel.Events;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArenaPro.Test
{
    [TestClass]
    public class TranfersOfferTest
    {

        public TranfersOfferTest()
        {
            var container = new UnityContainer();
            DependencyRegister.Register(container);            
            EventTrigger.ContainerResolver = new UnityResolverHelper(container);
        }
        [TestMethod]
        public void Exchange_Player()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var messi = barcelona.GetPlayer(1);
            var aguero = manchester.GetPlayer(2);

            var buyerOffer = new TradeOffer(manchester);
            buyerOffer.AddPlayer(aguero);

            var sellerOffer = new TradeOffer(barcelona);
            sellerOffer.AddPlayer(messi);

            var notificacoes = EventTrigger.ContainerResolver.GetService<IEventHandler<DomainNotification>>();

            market.ExchangePlayer(buyerOffer, sellerOffer);

            Assert.AreEqual(true, manchester.HasPlayer(messi) &&
                                    !barcelona.HasPlayer(messi) &&
                                    !manchester.HasPlayer(aguero) &&
                                        barcelona.HasPlayer(aguero) && !notificacoes.HasNotifications());
        }

        [TestMethod]
        public void Exchange_Can_Not_Contans_Money()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var messi = barcelona.GetPlayer(1);
            var aguero = manchester.GetPlayer(2);

            var buyerOffer = new TradeOffer(manchester);
            buyerOffer.AddPlayer(aguero);

            var sellerOffer = new TradeOffer(barcelona);
            sellerOffer.AddPlayer(messi);
            sellerOffer.AddMoney(100);

            var notificacoes = EventTrigger.ContainerResolver.GetService<IEventHandler<DomainNotification>>();

            market.ExchangePlayer(buyerOffer, sellerOffer);

            Assert.AreEqual(true, notificacoes.HasNotifications());
        }

        [TestMethod]
        public void Offers_Cannot_Contain_Money_Added_Offer1()
        {
            var offer1 = new TradeOffer(new Club() { Money = 100 });
            offer1.AddMoney(10);
            var offer2 = new TradeOffer(new Club() { Money = 100 });

            var rule = new OffersCannotContainMoney(offer1, offer2);

            Assert.AreEqual(false, rule.Validate());
        }

        [TestMethod]
        public void Offers_Cannot_Contain_Money_Added_Offer2()
        {
            var offer1 = new TradeOffer(new Club() { Money = 100 });
            var offer2 = new TradeOffer(new Club() { Money = 100 });
            offer2.AddMoney(10);

            var rule = new OffersCannotContainMoney(offer1, offer2);
            Assert.AreEqual(false, rule.Validate());
        }

        [TestMethod]
        public void Offers_Cannot_Contain_Money_Added_BothOffers()
        {
            var offer1 = new TradeOffer(new Club() { Money = 100 });
            offer1.AddMoney(10);
            var offer2 = new TradeOffer(new Club() { Money = 100 });
            offer2.AddMoney(10);

            var rule = new OffersCannotContainMoney(offer1, offer2);
            Assert.AreEqual(false, rule.Validate());
        }

        [TestMethod]
        public void Offers_Cannot_Contain_Money()
        {
            var offer1 = new TradeOffer(new Club() { Money = 100 });
            var offer2 = new TradeOffer(new Club() { Money = 100 });

            var rule = new OffersCannotContainMoney(offer1, offer2);
            Assert.AreEqual(true, rule.Validate());
        }

        [TestMethod]
        public void Offers_Contains_Money_BothOffers()
        {
            var offer1 = new TradeOffer(new Club() { Money = 100 });
            offer1.AddMoney(10);
            var offer2 = new TradeOffer(new Club() { Money = 100 });
            offer2.AddMoney(10);

            var rule = new OffersContainAtLeastMoney(offer1, offer2);

            Assert.AreEqual(true, rule.Validate());
        }

        [TestMethod]
        public void Offers_Contains_Money_Offer1()
        {
            var offer1 = new TradeOffer(new Club() { Money = 100 });
            offer1.AddMoney(10);
            var offer2 = new TradeOffer(new Club() { Money = 100 });

            var rule = new OffersContainAtLeastMoney(offer1, offer2);

            Assert.AreEqual(true, rule.Validate());
        }

        [TestMethod]
        public void Offers_Contains_Money_Offer2()
        {
            var offer1 = new TradeOffer(new Club() { Money = 100 });
            var offer2 = new TradeOffer(new Club() { Money = 100 });
            offer2.AddMoney(10);

            var rule = new OffersContainAtLeastMoney(offer1, offer2);

            Assert.AreEqual(true, rule.Validate());
        }

        [TestMethod]
        public void Offers_Contains_Money_WithoutMoney()
        {
            var offer1 = new TradeOffer(new Club() { Money = 100 });
            var offer2 = new TradeOffer(new Club() { Money = 100 });

            var rule = new OffersContainAtLeastMoney(offer1, offer2);

            Assert.AreEqual(false, rule.Validate());
        }

        [TestMethod]
        public void Offers_Contains_At_Least_One_Player_Offer1()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var aguero = manchester.GetPlayer(2);

            var offer1 = new TradeOffer(manchester);
            offer1.AddPlayer(aguero);

            var offer2 = new TradeOffer(new Club() { Money = 100 });

            var rule = new OffersContainsAtLeastPlayer(offer1, offer2);

            Assert.AreEqual(true, rule.Validate());
        }

        [TestMethod]
        public void Offers_Contains_At_Least_One_Player_Offer2()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var aguero = manchester.GetPlayer(2);

            var offer1 = new TradeOffer(barcelona);

            var offer2 = new TradeOffer(manchester);
            offer2.AddPlayer(aguero);

            var rule = new OffersContainsAtLeastPlayer(offer1, offer2);

            Assert.AreEqual(true, rule.Validate());
        }

        [TestMethod]
        public void Offers_Contains_At_Least_One_Player_WithourPlayer()
        {
            var offer1 = new TradeOffer(new Club() { Money = 100 });
            var offer2 = new TradeOffer(new Club() { Money = 100 });

            var rule = new OffersContainsAtLeastPlayer(offer1, offer2);

            Assert.AreEqual(false, rule.Validate());
        }

        [TestMethod]
        public void Offers_Max_Player_MaxNumber()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var aguero = manchester.GetPlayer(2);
            var messi = barcelona.GetPlayer(1);
            var neymar = barcelona.GetPlayer(3);
            //var suarez = barcelona.GetPlayer(5);

            var offer1 = new TradeOffer(barcelona);
            offer1.AddPlayer(messi);
            offer1.AddPlayer(neymar);

            var offer2 = new TradeOffer(manchester);
            offer2.AddPlayer(aguero);

            var rule = new OffersMaxPlayer(offer1, offer2, market.MaxPlayerTrade);

            Assert.AreEqual(true, rule.Validate());
        }

        [TestMethod]
        public void Offers_Max_Player_Exceeded()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var aguero = manchester.GetPlayer(2);
            var messi = barcelona.GetPlayer(1);
            var neymar = barcelona.GetPlayer(3);
            var suarez = barcelona.GetPlayer(5);

            var offer1 = new TradeOffer(barcelona);
            offer1.AddPlayer(messi);
            offer1.AddPlayer(neymar);
            offer1.AddPlayer(suarez);

            var offer2 = new TradeOffer(manchester);
            offer2.AddPlayer(aguero);

            var rule = new OffersMaxPlayer(offer1, offer2, market.MaxPlayerTrade);

            Assert.AreEqual(false, rule.Validate());
        }

        [TestMethod]
        public void Offers_Min_Player_MinNumber()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var aguero = manchester.GetPlayer(2);
            var messi = barcelona.GetPlayer(1);

            var offer1 = new TradeOffer(barcelona);
            offer1.AddPlayer(messi);

            var offer2 = new TradeOffer(manchester);
            offer2.AddPlayer(aguero);

            var rule = new OffersMinPlayer(offer1, offer2, market.MinPlayerTrade);

            Assert.AreEqual(true, rule.Validate());
        }

        [TestMethod]
        public void Offers_Min_Player_WithoutPlayer()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var offer1 = new TradeOffer(barcelona);
            var offer2 = new TradeOffer(manchester);

            var rule = new OffersMinPlayer(offer1, offer2, market.MinPlayerTrade);

            Assert.AreEqual(false, rule.Validate());
        }

        [TestMethod]
        public void Range_Price_Trade_betweenRange()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var aguero = manchester.GetPlayer(2);
            aguero.Price = 4000;

            var messi = barcelona.GetPlayer(1);
            messi.Price = 5000;

            var buyer = new TradeOffer(manchester);
            buyer.AddPlayer(aguero);

            var seller = new TradeOffer(barcelona);
            seller.AddPlayer(messi);

            var rule = new RangePriceTrade(buyer, seller, market.TradePercent);

            Assert.AreEqual(true, rule.Validate());
        }

        [TestMethod]
        public void Range_Price_Trade_BelowRange()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var aguero = manchester.GetPlayer(2);
            aguero.Price = 3900;

            var messi = barcelona.GetPlayer(1);
            messi.Price = 5000;

            var buyer = new TradeOffer(manchester);
            buyer.AddPlayer(aguero);

            var seller = new TradeOffer(barcelona);
            seller.AddPlayer(messi);

            var rule = new RangePriceTrade(buyer, seller, market.TradePercent);

            Assert.AreEqual(false, rule.Validate());
        }

        [TestMethod]
        public void Range_Price_Trade_AboveRange()
        {
            TransferMarket market;
            Club manchester;
            Club barcelona;

            SetUpMock(out market, out manchester, out barcelona);

            var aguero = manchester.GetPlayer(2);
            aguero.Price = 7000;

            var messi = barcelona.GetPlayer(1);
            messi.Price = 5000;

            var buyer = new TradeOffer(manchester);
            buyer.AddPlayer(aguero);

            var seller = new TradeOffer(barcelona);
            seller.AddPlayer(messi);

            var rule = new RangePriceTrade(buyer, seller, market.TradePercent);

            Assert.AreEqual(false, rule.Validate());
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
