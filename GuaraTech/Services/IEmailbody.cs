using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Services
{
    public interface IEmailbody
    {
        public string BodyEmail(string nome, string msg);
    }
}
