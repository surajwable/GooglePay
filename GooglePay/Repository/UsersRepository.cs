using AutoMapper;
using GooglePay.Data;
using GooglePay.DTOs;
using GooglePay.Interfaces;
using GooglePay.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GooglePay.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public UsersRepository(DataContext dataContext,IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetUsers()
        {
            var users = await dataContext.AccountHolders.ToListAsync();
            var userDto = mapper.Map<IEnumerable<MemberDto>>(users);
            return userDto;
        }

        public async Task<AccountHolder> GetUserById(int id)
        {
            var user = await dataContext.AccountHolders.SingleOrDefaultAsync(x => x.AccountHolderId == id);
            if(user != null)
            {
                return user;
            }
            return null;
        }

        //public async Task<TransactionDto> Credit(CreditDto creditDto)
        //{
        //    var sender = await dataContext.AccountHolders.FindAsync(creditDto.SenderId);
        //    var reciever = await dataContext.AccountHolders.FindAsync(creditDto.ReceiverId);

        //    if(sender == null || reciever == null || sender.Balance < 500 || sender.Balance < creditDto.Amount) 
        //    {
        //        return null;
        //    }
        //    sender.Balance -= creditDto.Amount;
        //    reciever.Balance += creditDto.Amount;
        //    await dataContext.SaveChangesAsync();

        //    var transaction = new Transaction
        //    {
        //        SenderId = creditDto.SenderId,
        //        ReceiverId = creditDto.ReceiverId,
        //        Amount = creditDto.Amount,
        //        TimeStamp = DateTime.UtcNow,
        //    };

        //    dataContext.Transactions.Add(transaction);

        //    sender.SentTransactions.Add(transaction);   
        //    reciever.RecievedTransactions.Add(transaction);
        //    await dataContext.SaveChangesAsync();

        //    return new TransactionDto
        //    {
        //        SenderId = sender.AccountHolderId,
        //        ReceiverId = reciever.AccountHolderId,
        //        SenderBalance = sender.Balance,
        //        RecieverBalance = reciever.Balance,
        //    };
        //}
        //

        public async Task<Boolean> Credit(CreditDto creditDto)
        {
            var sender = await dataContext.AccountHolders.FindAsync(creditDto.SenderId);
            var reciever = await dataContext.AccountHolders.FindAsync(creditDto.ReceiverId);

            if (sender == null || reciever == null || sender.Balance < 500 || sender.Balance < creditDto.Amount)
            {
                return false;
            }
            sender.Balance -= creditDto.Amount;
            reciever.Balance += creditDto.Amount;
            await dataContext.SaveChangesAsync();

            var transaction = new Transaction
            {
                SenderId = creditDto.SenderId,
                ReceiverId = creditDto.ReceiverId,
                Amount = creditDto.Amount,
                TimeStamp = DateTime.UtcNow,
            };

            dataContext.Transactions.Add(transaction);

            sender.SentTransactions.Add(transaction);
            reciever.RecievedTransactions.Add(transaction);
            await dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Transaction>> GetHistory(int currentUserId, int targetUserId)
        {
           var history = await dataContext.Transactions
                .Where(x => (x.SenderId == currentUserId && x.ReceiverId == targetUserId)
                    || 
                (x.SenderId == targetUserId && x.ReceiverId == currentUserId))
                .OrderBy(x=>x.TimeStamp)
                .ToListAsync();
           if(history != null)
            {
                return history;
            }
            return null;
        }
    }
}
