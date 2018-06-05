

using System;
namespace ArenaPro.Domain.Validation
{
    public class GenericValidationFactory
    {

        public static IValidator InitialDateMustBeLessThanFinalDate(DateTime initialDate, DateTime finalDate)
        {
            return new InitialDateMustBeLessEqualThanFinalDate(initialDate, finalDate);
        }
    }
}
