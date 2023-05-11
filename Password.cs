using MySqlConnector;

namespace MySQL_PasswordManager
{
    // A class to deal with all of the password operations.
    public class Password
    {
        // String to connect to the MySQL db.
        // It is private and readonly as we don't want it to be seen and we dont want it to change.
        private readonly string conn = @"server=localhost;userid=root;password=y4qWs9Jst725peQ^;database=passwordmanagerdb";

        // Method for adding a new password to the db.
        public void AddPassword(string site, string username, string password)
        {
            // If either site, username or password are empty or null, the method throws an exception.
            if (String.IsNullOrWhiteSpace(site) || String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Site, username or password null.");
            }

            // Connecting to the DB.
            using var con = new MySqlConnection(conn);
            con.Open(); // Opening the db.
            using var cmd = new MySqlCommand();
            cmd.Connection = con;

            // Running a MySQL command from Visual Studio to insert a new row.
            string insertString = "INSERT INTO passwords(site, username, password) VALUES('" + site + "', '" + username + "', '" + password + "')";
            cmd.CommandText = insertString;
            cmd.ExecuteNonQuery();
            con.Close(); // Once we're done with the db, we close it.
        }

        // Method to check if a row exists in the db. 
        public bool SearchDB(string field, string input)
        {            
            // If the input is empty or null, throws an exception.
            if (String.IsNullOrWhiteSpace(input) || String.IsNullOrWhiteSpace(field))
            {
                throw new ArgumentNullException("User input or field is null.");
            }

            // If the input isn't the specified field, throws an exception.
            if (field != "site" && field != "username" && field != "password")
            {
                throw new ArgumentOutOfRangeException("Field does not exist.");
            }
            
            // Connecting to the db.
            using var con = new MySqlConnection(conn);
            con.Open(); // Opening the db.
            using var cmd = new MySqlCommand();
            cmd.Connection = con;

            // Passing a command toselect and read a row from Visual Studio to MySQL.
            string searchString = "SELECT * FROM passwords WHERE " + field + "='" + input + "';";
            using var cmd1 = new MySqlCommand(searchString,con);
            using MySqlDataReader rdr = cmd1.ExecuteReader();

            // If the row doesn't exist, returns bool value false.
            return rdr.Read();

        }

        // Method to delete a password from the db.
        public void DeletePassword(string field, string input)
        {
            // Connecting to the db.
            using var con = new MySqlConnection(conn);
            con.Open(); // Opening the db.
            using var cmd = new MySqlCommand();
            cmd.Connection = con;

            // Executing query to delete a row from the db.
            string deleteString = "DELETE FROM passwords WHERE " + field + "='" + input + "';";
            cmd.CommandText = deleteString;
            cmd.ExecuteNonQuery();
            con.Close(); // Once we're done, closing the db.

        }

        // Method to view a row in the db.
        public void ViewPassword(string field, string input)
        {
            // Connecting to the db.
            using var con = new MySqlConnection(conn);
            con.Open(); // Opening the db.
            using var cmd = new MySqlCommand();
            cmd.Connection = con;

            // Passing a command to select and read a row from Visual Studio to MySQL.
            string viewString = "SELECT * FROM passwords WHERE " + field + "='" + input + "';";
            using var cmd1 = new MySqlCommand(viewString, con);
            using MySqlDataReader rdr = cmd1.ExecuteReader();

            // Displaying the row on the console.
            rdr.Read();
            Console.WriteLine("Site: {0}   Username: {1}   Password: {2}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2));
            con.Close();

        }
    }
}
