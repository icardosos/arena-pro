using ArenaPro.SharedKernel.Events;
using System.Collections.Generic;

namespace ArenaPro.Domain.Validation
{
    internal static class ValidationHelper 
    {
        internal static bool ValidateProcess(IEnumerable<IValidator> validators)
        {
            foreach (var validator in validators)
            {
                if (!validator.Validate())
                {
                    EventTrigger.RaiseDomainNotification(validator.Message);
                    return false;
                }
            }
            return true;
        }
    }
}
