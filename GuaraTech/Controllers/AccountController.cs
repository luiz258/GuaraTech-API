using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuaraTech.DTO;
using GuaraTech.Models;
using GuaraTech.Repository.IRepository;
using GuaraTech.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuaraTech.Controllers
{
    [Route("v1/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repAccount;
        public AccountController(IAccountRepository repAccount)
        {
            _repAccount = repAccount;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] loginDTO model)
        {

            var email = await _repAccount.ValidateEmail(model.EmailUser);

            if (!email)
                return Ok(new { message = "Email não cadastrado!", success = false });

            // Recupera o usuário
            var user = await _repAccount.Authenticate(model);

            // Verifica se o usuário existe
            if (user == null)
                return Ok(new { message = "Usuário inválido", success = false });

            // Gera o Token
            if (user.PasswordUser == encryptPassword(model.PasswordUser)) {
                var token = TokenService.GenerateToken(user);

                // Oculta a senha
                user.PasswordUser = "";

                // Retorna os dados
                return Ok(new
                {
                    message = "Login efetuada com sucesso !",
                    user = user,
                    token = token,
                    success = true

                });
            }

            return Ok(new { message = "Senha inválida", success = false });



        }

        protected string encryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            ///var _senha = (pass += "|2d331cca-f6c0-40c0-bbp3-6e32909c2560");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }

        [HttpPost]
        [Route("registerClient")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> RegisterClient([FromBody] User model){

            var document = await _repAccount.ValidateDocument(model.Document);
            var email = await _repAccount.ValidateEmail(model.EmailUser);

            if (document)
                return Ok(new { message = "Cpf já cadastrado!", success = false });

            if (email)
                return Ok(new { message = "Email já cadastrado!", success = false });

            var password = encryptPassword(model.PasswordUser);
            var userRegister = new User {
                FullName = model.FullName, 
                Document = model.Document, 
                EmailUser = model.EmailUser,
                PasswordUser = password,
                RoleId = model.RoleId ,
                StateAccount = "1", 
                Avatar = model.Avatar,
                
            };

             await _repAccount.CreateUser(userRegister);

            return Ok(new { message = "Usuário cadastrado !", success = true });
        }

    }
}
