using GooglePay.DTOs;
using GooglePay.Models;
using Microsoft.AspNetCore.Mvc;

namespace GooglePay.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<MemberDto>> GetUsers();
        Task<AccountHolder> GetUserById(int id);

        Task<Boolean> Credit(CreditDto creditDto);

        Task<IEnumerable<Transaction>> GetHistory(int currentUserId, int targetUserId);
    }
}
