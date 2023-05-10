using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQL_PasswordManager
{
    public class MasterPassword
    {
        private string _masterUsername = "user";
        private string _masterPassword = "pass";
        public bool ValidatePW(string username, string password)
        {
            if (username != _masterUsername || password != _masterPassword)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void BreachAttempt(bool attempt)
        {
            if (attempt)
            {
                string s = DateTime.Now.ToString();
                File.AppendAllText("LoginAttempt.txt", "Breach Attempt: " + s+"\n");
            }
        }
    }
}
