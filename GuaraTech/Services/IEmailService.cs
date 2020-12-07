using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Services
{
    public interface IEmailService
    {
        bool Send(string nome, string email, string assunto, string msg);
    }
}
