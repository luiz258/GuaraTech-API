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
        private readonly IEmailService  _email;
        private readonly IHttpContextAccessor _accessor;
        public AccountController(IAccountRepository repAccount, IEmailService email, IHttpContextAccessor accessor)
        {
            _repAccount = repAccount;
            _email = email;
            _accessor = accessor;
        }

        [HttpPost]
        [Route("v1/user/list")]
        [Authorize]
        public async Task<IEnumerable<User>> ListUserByEmail(string email)
        {
            var list = await _repAccount.Lis(IdCanvas);
            return list;
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

        [HttpPost]
        [Route("registerClient")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> RegisterClient([FromBody] User model) {

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
                RoleId = model.RoleId,
                StateAccount = "1",
                Avatar = model.Avatar,

            };

            await _repAccount.CreateUser(userRegister);

            return Ok(new { message = "Usuário cadastrado !", success = true });
        }

        [HttpGet]
        [Route("get-profile")]
        [Authorize]
        public async Task<User> GetProfilet()
        {
            var id = Guid.Parse(_accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value);
            var data = await _repAccount.GetUserById(id);
            data.PasswordUser = "";
            return data;

        }

        [HttpPut]
        [Route("profile")]
        [Authorize]
        public async Task<ActionResult<User>> Profile([FromBody] UserUpdateDto model)
        {
            var id = Guid.Parse(_accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value);
            var data = await _repAccount.GetUserById(id);
            if (data == null) return Ok(new { message = "Usuario não existe !", success = false });

            var userUpdate = new User { EmailUser = model.EmailUser };
            await _repAccount.UpdateProfile(userUpdate, id);

            return Ok(new { message = "Usuario alterado com successo !", success = true });
        }

        [HttpPut]
        [Route("alter-password")]
        [Authorize]
        public async Task<ActionResult<AlterPasswordDto>> AlterPassword([FromBody] AlterPasswordDto model)
        {
            var id = _accessor.HttpContext.User.Claims.Where(a => a.Type == "Id").FirstOrDefault().Value;
            var guid = Guid.Parse(id);
            var user = await _repAccount.GetUserById(guid);

            var LastPassword = encryptPassword(model.LastPassword);

            if (LastPassword != user.PasswordUser) return Ok(new { message = "A antiga senha não confere !", success = false });

            var ConfirmNewPassword = encryptPassword(model.ConfirmNewPassword);
            var NewPassword = encryptPassword(model.NewPassword);

            if (ConfirmNewPassword != NewPassword) return Ok(new { message = "Senha não conferem !", success = false });

             await _repAccount.AlterPassWord(NewPassword, guid);

            return Ok(new { message = "Senha alterada com sucesso !", success = true });

        }

        [HttpPost]
        [Route("reset-password")]
        [AllowAnonymous]
        public async Task<ActionResult<ResetPasswordDto>> ResetPassword([FromBody] ResetPasswordDto model)
        {

            var user = await _repAccount.GetAccount(model.email);

            if (user == null) return Ok(new { message = "Email não existe!", success = false });
            if (user.EmailUser != model.email) return Ok(new { message = "Email não confere!", success = false });
            if (user.Document != model.cpf) return Ok(new { message = "Cpf não confere!", success = false });

            var charactersRamdow = alfanumericoAleatorio(4);
            String NewPassword = $"guara{charactersRamdow}";
            var password = encryptPassword($"{NewPassword}");

            await _repAccount.AlterPassWord(password, user.Id);
            var mailSent = _email.Send(user.FullName, user.EmailUser,"SENHA ALTERADA - GUARÁTECH !", $"{NewPassword}");

            if (!mailSent) return Ok(new { message = "Erro ao enviar senha no e-mail", success = false });

            return Ok(new { message = "Senha alterada com sucesso !", success = true });

        }

        protected string alfanumericoAleatorio(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
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

      

    } 
}
