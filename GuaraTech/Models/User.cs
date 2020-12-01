using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Models
{
    public class User:Entity
    {
        [Required]
        public String FullName { get; set; }

        [Required]
        public String EmailUser  { get; set; }
        [Required]
        public String PasswordUser  { get; set; } 

        public String Avatar  { get; set; } 

        [Required]
        public String  Document { get; set; }

        public String RoleId { get; set; }

        public String StateAccount { get; set; }
    }
}
