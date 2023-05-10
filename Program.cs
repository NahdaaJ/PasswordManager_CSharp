
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using MySqlConnector;

namespace MySQL_PasswordManager
{
    class Program
    {
        static void Main(string[] args)
        {
            int tries = 3;
            bool correct = false;
            var Validate = new MasterPassword();

            Program.CheckFile();

            // Validating Master Password
            while (tries > 0)
            {
                Console.WriteLine("Username: ");
                var masterInput1 = Console.ReadLine();
                Console.WriteLine("Password: ");
                var masterInput2 = Console.ReadLine();

                if (!Validate.ValidatePW(masterInput1, masterInput2))
                {
                    Console.WriteLine("Incorrect username or password.");
                    Console.WriteLine("{0} attempts remaining.\n", tries - 1);
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
            if (!correct)
            {
                Console.WriteLine("Recording breach attempt.");
                Validate.BreachAttempt(true);
                return;
            }


            // Menu
            while (true)
            {
                Console.WriteLine("\nWould you like to:\n- Add a password (add)\n- Delete a password (delete)" +
                    "\n- View a password (view)\n- Quit (quit)");
                var userChoice = Console.ReadLine().ToLower();


                if (String.IsNullOrWhiteSpace(userChoice) || userChoice != "add" && userChoice != "delete" && userChoice != "view" 
                    && userChoice != "quit")
                {
                    Console.WriteLine("Invalid input, please try again.\n");
                    continue;
                }


                else if (userChoice == "add")
                {
                    while (true)
                    {
                        Console.WriteLine("\nSite:");
                        var siteInput = Console.ReadLine();
                        Console.WriteLine("Username:");
                        var usernameInput = Console.ReadLine();
                        Console.WriteLine("Password:");
                        var passwordInput = Console.ReadLine();

                        Console.WriteLine("\nSite: {0}   Username: {1}   Password: {2}\nIs this correct? (Y/N)",siteInput, usernameInput, passwordInput);
                        var userConfirmation = Console.ReadLine().ToLower();
                        if(userConfirmation == "y")
                        {
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
                        else
                        {
                            Console.WriteLine("\nPlease re-enter.");
                            continue;
                        }
                    }
                }


                else if (userChoice == "delete")
                {
                    Console.WriteLine("Password Deleted.\n");
                }


                else if (userChoice == "view")
                {
                    Console.WriteLine("Password:\n");
                }


                else
                {
                    Console.WriteLine("Thank you for using Password Manager.");
                    return;
                }
            }

           


        }

        public static void CheckFile()
        {
            // If a .txt file doesn't exist, create it.
            // Will check for database and tables later.
            if (!File.Exists("LoginAttempt.txt"))
            {
                File.Create("LoginAttempt.txt");
            }

            if (!File.Exists("Passwords.txt"))
            {
                File.Create("Passwords.txt");
            }
        }
    }

}


