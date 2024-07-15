using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModelLayer
{
    public class LoginModel
    {
        public LoginModel(string username, string password)
        {
            UserName = username;    
            Password = password;    
        }
        public string UserName { get; set; } 
        public string Password { get; set; } 
    }
}
