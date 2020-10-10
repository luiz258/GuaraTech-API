using Dapper;
using GuaraTech.DTO;
using GuaraTech.Infra;
using GuaraTech.Models;
using GuaraTech.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly DBContext _db;
        public AccountRepository(DBContext db)
        {
            _db = db;
        }
        public Task<User> Authenticate(loginDTO user)
        {
            return _db.Connection.QueryFirstAsync<User>("SELECT * FROM ACCOUNT WHERE EmailUser=@Email",
                new {@Email = user.EmailUser});
        }

        public Task CreateUser(User user)
        {
            try
            {

                return _db.Connection.ExecuteAsync("INSERT INTO ACCOUNT (ID, EmailUser, PasswordUser, FullName, document, RoleId, StateAccount) VALUES  (@ID, @EmailUser, @PasswordUser, @FullName, @Document, @RoleId, @StateAccount)"
                    , new
                    {
                        @ID = user.Id,
                        @EmailUser = user.EmailUser,
                        @PasswordUser = user.PasswordUser,
                        @FullName = user.FullName,
                        @Document = user.Document,
                        @RoleId = user.RoleId,
                        @StateAccount = user.StateAccount
                    });
            }
            catch (Exception )
            {

                throw;
            }
        }

        public Task<User> GetAccount(string EmailUser)
        {
            return _db.Connection.QueryFirstAsync<User>("SELECT * FROM ACCOUNT WHERE EmailUser = @EmailUser ", new { @EmailUser = EmailUser });
        }

        public  Task<bool> ValidateDocument(string document)
        {
            return _db.Connection.QueryFirstAsync<bool>("SELECT CASE WHEN EXISTS( SELECT document FROM ACCOUNT WHERE document = @document) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @document = document });
        }

        public Task<bool> ValidateEmail(string Email)
        {

            return _db.Connection.QueryFirstAsync<bool>("SELECT CASE WHEN EXISTS( SELECT EmailUser FROM ACCOUNT WHERE EmailUser = @Email) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @Email = Email });
           
        }
    }
}
