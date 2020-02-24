using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FightQuoteLibrary
{
    public class Utility
    {
        public static void GetConnectionStringInfo(SqlConnectionStringBuilder builder)
        {
            Console.WriteLine();
            Console.WriteLine("Connection String:");
            Console.WriteLine(builder.ConnectionString);
            Console.WriteLine();
            Console.WriteLine("Press any key to query database...");
            Console.WriteLine();
            Console.ReadKey();
        }

        public static void PrintBanner()
        {
            Console.Clear();
            string banner = @"
  ______   _           _       _        ____                    _          
 |  ____| (_)         | |     | |      / __ \                  | |         
 | |__     _    __ _  | |__   | |_    | |  | |  _   _    ___   | |_    ___ 
 |  __|   | |  / _` | | '_ \  | __|   | |  | | | | | |  / _ \  | __|  / _ \
 | |      | | | (_| | | | | | | |_    | |__| | | |_| | | (_) | | |_  |  __/
 |_|      |_|  \__, | |_| |_|  \__|    \___\_\  \__,_|  \___/   \__|  \___|
                __/ |                                                      
               |___/ 

";
            Console.WriteLine(banner);
            Console.WriteLine("");
        }

        public static void PrintEnding()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to close App...");
            Console.ReadKey();
        }

        public static string GetFightFact(string connectionString)
        {
            string quote = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand();
                command.CommandText = "dbo.get_random_fight_fact";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    quote = string.Format("On {0:MM/dd/yyyy}, {1} beat {2} in a {3} {4} in {5}. It was officiated by {6}.",
                        Convert.ToDateTime(reader["date"]),
                        reader["Winner"],
                        reader["Loser"],
                        reader["Format"],
                        reader["Fight_type"],
                        reader["location"],
                        reader["Referee"]
                    );
                }
            }
            return quote;
        }

        public static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

    }
}
