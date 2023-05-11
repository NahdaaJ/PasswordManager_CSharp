
namespace MySQL_PasswordManager
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialising important values.
            int tries = 3;
            bool correct = false;

            // Check if the database exists.
            var checkDB = new CheckDB();
            if (!checkDB.CheckForDB())
            {
                Console.WriteLine("Database does not exist.");
                return;
            }

            // Validating Master Password
            // User has 3 attempts to write correct master username and password.
            var Validate = new MasterPassword();
            while (tries > 0)
            {
                Console.Write("Username: ");
                var masterInput1 = Console.ReadLine();
                Console.Write("Password: ");
                var masterInput2 = Console.ReadLine();

                if (!Validate.ValidatePW(masterInput1, masterInput2))
                {
                    Console.WriteLine("Incorrect username or password.");
                    Console.WriteLine("{0} attempt(s) remaining.\n", tries - 1);
                    tries--;
                    correct = false;
                }
                else
                {
                    Console.WriteLine("Welcome back, Nahdaa.");
                    correct = true;
                    break;
                }
            }

            // If username and password are still incorrect after 3 tries, program records it as a breach.
            if (!correct)
            {
                Console.WriteLine("Recording breach attempt.");
                Validate.BreachAttempt();
                return;
            }


            // Menu for user to choose service.
            while (true)
            {
                Console.WriteLine("\nWould you like to:\n- Add a password (add)\n- Delete a password (delete)" +
                    "\n- View a password (view)\n- View breach attempts (breach)\n- Quit (quit)");
                var userChoice = Console.ReadLine().ToLower();

                // If user enters choice incorrectly, they are prompted to enter again.
                if (String.IsNullOrWhiteSpace(userChoice) || userChoice != "add" && userChoice != "delete" && userChoice != "view"
                    && userChoice != "quit" && userChoice != "breach")
                {
                    Console.WriteLine("Invalid input, please try again.\n");
                    continue;
                }


                //If user choosed to add a password.
                else if (userChoice == "add")
                {
                    while (true)
                    {
                        Console.Write("\nSite: ");
                        var siteInput = Console.ReadLine();
                        Console.Write("Username: ");
                        var usernameInput = Console.ReadLine();
                        Console.Write("Password: ");
                        var passwordInput = Console.ReadLine();

                        // Asking user for confirmation.
                        Console.Write("\nSite: {0}   Username: {1}   Password: {2}\nIs this correct, or would you like to cancel? (Y/N/Cancel): ", siteInput, usernameInput, passwordInput);
                        var userConfirmation = Console.ReadLine().ToLower();
                        if (userConfirmation == "y")
                        {
                            // Makes sure null values aren't passed into the method.
                            try
                            {
                                var newPassword = new Password();
                                newPassword.AddPassword(siteInput, usernameInput, passwordInput);

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Site, username or password is empty. Please try again.\n");
                                continue;
                            }
                            Console.WriteLine("Password added.");
                            break;

                        }
                        // If user cancels, takes them back to the menu.
                        else if (userConfirmation == "cancel")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nPlease re-enter.");
                            continue;
                        }
                    }
                }

                // If user chooses to delete a password.
                else if (userChoice == "delete")
                {
                    while (true)
                    {
                        Console.Write("\nPlease enter which password you want to delete.\nYou can choose to enter the site, username or password: ");
                        var deleteInput = Console.ReadLine();
                        Console.Write("Is your input the site, username or password? ");
                        var fieldInput = Console.ReadLine();

                        var deletePassword = new Password();

                        try
                        {
                            // Checking if the site, username, password set exists in the DB.
                            if (!deletePassword.SearchDB(fieldInput, deleteInput))
                            {
                                Console.WriteLine("That {0} does not exist. Please try again.\n", fieldInput);
                                continue;
                            }
                        }
                        catch(Exception)
                        {
                            // Catches if null values are passed or if the site/username/password field is entered incorrectly.
                            Console.WriteLine("Site, username or password is empty or input does not exist. Please try again.\n");
                            continue;
                        }

                        // Displays site, username and password so that user can check what they are deleting.
                        Console.WriteLine();
                        deletePassword.ViewPassword(fieldInput, deleteInput);

                        // Asking for user confirmation.
                        Console.Write("Is this correct, or would you like to cancel? (Y/N/Cancel): ");
                        var userConfirmation = Console.ReadLine().ToLower();
                        if (userConfirmation == "y")
                        {
                            var delPassword = new Password();
                            delPassword.DeletePassword(fieldInput, deleteInput);
                            Console.WriteLine("Password deleted.");
                            break;

                        }
                        else if (userConfirmation == "cancel")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nPlease re-enter.");
                            continue;
                        }
                    }
                }
                // User chooses to view a password.
                else if (userChoice == "view")
                {
                    while (true)
                    {
                        Console.Write("\nPlease enter which password you want to view.\nYou can choose to enter the site, username or password: ");
                        var input = Console.ReadLine();
                        Console.Write("Is your input the site, username or password? ");
                        var field = Console.ReadLine();

                        var viewPassword = new Password();

                        try
                        {
                            // Checking if the site, username, password row set exists in the DB.
                            if (!viewPassword.SearchDB(field, input))
                            {
                                Console.WriteLine("That {0} does not exist. Please try again.\n", field);
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            // Catches if null values are passed or if the site/username/password field is entered incorrectly.
                            Console.WriteLine("Site, username or password is empty or input does not exist. Please try again.\n");
                            continue;
                        }
                        Console.WriteLine();
                        viewPassword.ViewPassword(field, input);
                        break;
                    }

                }
                // Displays all the recorded breaches in the DB.
                else if (userChoice == "breach")
                {
                    var viewBreach = new MasterPassword();
                    Console.WriteLine();
                    Console.WriteLine("Breach attempts: ");
                    viewBreach.ViewBreachAttempt();
                }
                // User chooses to quit the program.
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Thank you for using Password Manager.");
                    return;
                }
            }

        }
    }
}


