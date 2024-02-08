using GooglePay.Models;

namespace GooglePay.Services
{
    public interface ITokenService
    {
        string CreateToken(AccountHolder accountHolder);
    }
}
