using GooglePay.Data;
using GooglePay.DTOs;
using GooglePay.Extensions;
using GooglePay.Interfaces;
using GooglePay.Models;
using GooglePay.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace GooglePay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class UsersController : Controller
    {
        private readonly IUsersRepository usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [HttpGet("get-users")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {         
            var users = await usersRepository.GetUsers();
            if (users != null)
            {
                return Ok(users);
            }
             
            return NotFound();
        }

         [HttpPost("credit")]
         public async Task<ActionResult> Credit([FromBody]CreditDto creditDto)
         {
            var transactionDto = await usersRepository.Credit(creditDto);
            if(!transactionDto)
            {
                return BadRequest("balance should be above 500");
            }
            return Ok("Money Sent Successfully");
         }


        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetHistory(int currentUserId,int targetUserId)
        {
            var history = await usersRepository.GetHistory(currentUserId, targetUserId);
            if(history != null)
            {
                return Ok(history);
            }
            return BadRequest("No transactions found");
        }
                
      }

    }

