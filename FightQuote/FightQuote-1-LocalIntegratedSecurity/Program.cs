using System;
using System.Data.SqlClient;
using FightQuoteLibrary;

namespace FightQuote
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine("Starting up Fight Quote App...");

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "yourdb";

            Utility.GetConnectionStringInfo(builder);

            Utility.PrintBanner();

            Console.WriteLine(Utility.GetFightFact(builder.ConnectionString));

            Utility.PrintEnding();
        }

    }
}
