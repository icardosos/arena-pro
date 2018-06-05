using System;

namespace ArenaPro.Domain.Validation
{
    public class InitialDateMustBeLessEqualThanFinalDate : IValidator
    {
        DateTime _initial;

        DateTime _final;

        public InitialDateMustBeLessEqualThanFinalDate(DateTime initialDate,DateTime finalDate)
        {
            this._initial = initialDate;
            this._final = finalDate;

        }
        public string Message
        {
            get { return "Final date is greater than initial date."; }
        }

        public bool Validate()
        {
            return this._initial.Date <= this._final.Date;
        }

    }
}
