using System;
using System.Data.SqlClient;
using FightQuoteLibrary;


namespace FightQuoteCloud
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting up Fight Quote App (Name/Password)...");

            var builder = new SqlConnectionStringBuilder();

            bool interactive = true;

            if (interactive)
            {
                builder.DataSource = @"<yoursqlserver>.database.windows.net";
                builder.IntegratedSecurity = false; 
                builder.InitialCatalog = "yourdb";

                //add some credentials interactively
                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter your password: ");
                string pass = GetPassword();
                Console.WriteLine();

                builder.UserID = username;
                builder.Password = pass;
            }
            else
            {
                //or use a config file entry
                builder.ConnectionString = Utility.GetConnectionStringByName("fightdata");
            }

            //Set this property for AAD - Password 
            //builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword;


            Utility.GetConnectionStringInfo(builder);

            Utility.PrintBanner();

            Console.WriteLine(Utility.GetFightFact(builder.ConnectionString));

            Utility.PrintEnding();
        }

        private static string GetPassword()
        {
            string pass = "";
            do
            {
                //https://stackoverflow.com/a/3404522

                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            return pass;
        }

    }
}
