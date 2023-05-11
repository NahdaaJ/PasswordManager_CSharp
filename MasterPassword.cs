using MySqlConnector;

namespace MySQL_PasswordManager
{
    public class MasterPassword
    {
        // Initialising some important variables.
        private string _masterUsername = "user";
        private string _masterPassword = "pass";
        private readonly string conn = @"server=localhost;userid=root;password=y4qWs9Jst725peQ^;database=passwordmanagerdb";

        // Method to check if the user input username and password matches the set master username and password.
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

        // Method to record a breach attempt when the master username and password is input incorrectly 3 times.
        public void BreachAttempt()
        {
            // Connecting to and opening the db.
            using var con = new MySqlConnection(conn);
            con.Open();
            using var cmd = new MySqlCommand();
            cmd.Connection = con;

            // Recording the current date and time.
            var now = DateTime.Now.ToString();
            string date = now.Substring(0, 10);
            string time = now.Substring(11);

            // Passing the current date and time to the login_attempt table in the db in MySQL.
            cmd.CommandText = "INSERT INTO login_attempts (breach_date, breach_time) VALUES('" + date + "','" + time + "')";
            cmd.ExecuteNonQuery();
            con.Close(); // Closing the db once we're done.
        }

        public void ViewBreachAttempt()
        {
            // Connecting to and opening the db.
            using var con = new MySqlConnection(conn);
            con.Open();
            using var cmd = new MySqlCommand();
            cmd.Connection = con;

            // Passing a command to MySQL to read every row. The more recent the breach, the closer to the top it is.
            string viewString = "SELECT * FROM login_attempts\nORDER BY breach_date DESC, breach_time DESC;";
            using var cmd1 = new MySqlCommand(viewString, con);
            using MySqlDataReader rdr = cmd1.ExecuteReader();

            // Displays every role in the table on the console.
            while (rdr.Read())
            {
                Console.WriteLine("Date: {0}   Time: {1}", rdr.GetString(0), rdr.GetString(1));
            }
            con.Close(); // Closing the file once we're done.
        }
    }
}
