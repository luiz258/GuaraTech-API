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

                return _db.Connection.ExecuteAsync("INSERT INTO ACCOUNT (ID, EmailUser, PasswordUser, FullName, document, RoleId, StateAccount, Avatar) VALUES  (@ID, @EmailUser, @PasswordUser, @FullName, @Document, @RoleId, @StateAccount, @Avatar)"
                    , new
                    {
                        @ID = user.Id,
                        @EmailUser = user.EmailUser,
                        @PasswordUser = user.PasswordUser,
                        @FullName = user.FullName,
                        @Document = user.Document,
                        @RoleId = user.RoleId,
                        @StateAccount = user.StateAccount,
                        @Avatar = user.Avatar
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

        public Task<User> GetUserById(Guid id)
        {
            return _db.Connection.QueryFirstAsync<User>("SELECT * FROM ACCOUNT WHERE Id = @Id ", new { @Id = id });
        }

        public Task<User> GetUserIdByFid(string fid)
        {
            return _db.Connection.QueryFirstAsync<User>("SELECT * FROM ACCOUNT WHERE Fid = @Fid ", new {@Fid = fid });
        }

        public  Task AlterPassWord(string password, Guid id)
        {
           
            return _db.Connection.ExecuteAsync("UPDATE ACCOUNT SET PasswordUser = @password where ID = @id", new  {@password = password, @id =id });
        }

        public Task UpdateProfile(User user, Guid id)
        {
            //var sql = "";
            //using (var com =  _db.Connection)
            //{

            //    var data = await com.ExecuteAsync(sql, user);
            //}

            return _db.Connection.ExecuteAsync("update ACCOUNT set EmailUser = @EmailUser WHERE ID = @ID ", new { @EmailUser = user.EmailUser, @ID =id });

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
