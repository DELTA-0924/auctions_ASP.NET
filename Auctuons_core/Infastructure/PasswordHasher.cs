using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auctuons_core.Abstractions;
using BCrypt;
using BCrypt.Net;
namespace Auctuons_core.Infastructure
{
    public  class PasswordHasher:IPasswordHasher
    {
        public string Generate(string password)=>BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);       
    }
}
