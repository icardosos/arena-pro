using ArenaPro.SharedKernel.Events;
using System.Collections.Generic;

namespace ArenaPro.Domain.Validation
{
    public abstract class Validatable
    {
        protected bool ValidateProcess(IEnumerable<IValidator> validators)
        {
            foreach (var validator in validators)
            {
                if (!validator.Validate())
                {
                    EventTrigger.Raise<DomainNotification>(new DomainNotification(validator.Message));
                    return false;
                }
            }
            return true;
        }

    }
}
