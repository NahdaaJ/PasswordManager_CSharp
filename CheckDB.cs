using MySqlConnector;

namespace MySQL_PasswordManager
{
    public class CheckDB
    {
        // Connection string set as private and read only as we don't want to access it or change it outside the class.
        private readonly string cd = @"server=localhost;userid=root;password=y4qWs9Jst725peQ^;database=passwordmanagerdb";
        
        // Checking if the database exists. If it doesn't, returns false.
        public bool CheckForDB()
        {
            try
            {
                using var con = new MySqlConnection(cd);
                con.Open();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
