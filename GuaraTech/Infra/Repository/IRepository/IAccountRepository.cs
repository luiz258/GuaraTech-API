using GuaraTech.DTO;
using GuaraTech.DTO.User;
using GuaraTech.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Repository.IRepository
{
    public interface IAccountRepository
    {
        Task<User> Authenticate(loginDTO user);
        Task CreateUser(User user);
        Task<bool> ValidateEmail(string Email);
        Task<bool> ValidateDocument(string document);
        Task<IEnumerable<ListUserEmail>> ListAccountByEmail(string EmailUser);
        Task<User> GetAccount(string EmailUser);
        Task<User> GetUserIdByFid(string fuid);
        Task<User> GetUserById(Guid id);
        
        Task UpdateProfile(User user, Guid id);
        Task AlterPassWord(string password, Guid id);

        //Task ResetPassword(string PasswordHash, Guid Id);
    }
}
