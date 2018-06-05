using ArenaPro.Domain.Validation;
using ArenaPro.Domain.Validation.Tournament;
using ArenaPro.SharedKernel.Events;
using System;
using System.Collections.Generic;

namespace ArenaPro.Domain
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Admin { get; set; }
        public string Division { get; set; }
        public TournamentType Type { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public int NumberOfUsers
        {
            get
            {
                return this.Clubs.Count;
            }
        }

        IList<Club> _clubs;
        public IList<Club> Clubs
        {
            get
            {
                if (_clubs == null)
                    _clubs = new List<Club>();
                return _clubs;
            }
            set { _clubs = value; }
        }

        IList<TransferMarket> _transferMarket;
        public IList<TransferMarket> TransferMarket
        {
            get
            {
                if (_transferMarket == null)
                    _transferMarket = new List<TransferMarket>();
                return _transferMarket;
            }
            set { _transferMarket = value; }
        }

        public bool IsOpen
        {
            get
            {
                return DateTime.Now.Date >= this.InitialDate.Date && DateTime.Now.Date <= this.FinalDate.Date;
            }
        }

        public TransferMarket CurrentTransferMarket
        {
            get
            {
                foreach (var item in this.TransferMarket)
                {
                    if (item.IsOpen)
                        return item;
                }
                return null;
            }
        }

        public TransferMarket OpenTransferMarket(DateTime initialDate, DateTime finalDate, decimal maxPlayerPrice, int stolenPercent, int tradePercent)
        {

            var validators
              = new List<IValidator> {
                    TournamentValidatorFactory.TournamentMustBeOpen(this), 
                    TournamentValidatorFactory.TransferMarketMustNotBeOpened(this), 
                    GenericValidationFactory.InitialDateMustBeLessThanFinalDate(initialDate,finalDate) };

            if (!ValidationHelper.ValidateProcess(validators))
                return null;

            var transferMarket = new TransferMarket()
            {
                InitalDate = initialDate,
                FinalDate = finalDate,
                MaxPlayerPrice = maxPlayerPrice
            };

            this.TransferMarket.Add(transferMarket);

            return transferMarket;
        }
    }
}
