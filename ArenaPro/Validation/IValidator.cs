
namespace ArenaPro.Domain.Validation
{
    public interface IValidator
    {   
        string Message { get; }
        bool Validate();
    }
}
