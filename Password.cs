using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQL_PasswordManager
{
    public class Password
    {
        // private string conn = @"server=localhost;userid=root;password=y4qWs9Jst725peQ^;database=passwordmanagerdb";

        public void AddPassword(string site, string username, string password)
        {
            if (String.IsNullOrWhiteSpace(site) || String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Site, username or password null.");
            }
            string s = "Site: "+site+"\tUsername: " +username+ "\tPassword: " + password+ "\n";
            File.AppendAllText("Passwords.txt", s);
        }

        public void DeletePassword(string input)
        {

        }

        public void ViewPassword(string input)
        {

        }
    }
}
