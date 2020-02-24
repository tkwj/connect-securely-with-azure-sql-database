using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Reflection;
using FightQuoteLibrary;

namespace FightQuoteCloud
{
    class Program
    {
        //ClientApplicationID and ClientSecret are from the service principal
        //Client also known as Application ID under App registrations
        public static readonly string ClientApplicationID = "your-appID-guid-here";

        //Secret is a password from the App registration > Settings > Keys
        public static readonly string ClientSecret = @"your app secret here";

        //tenant is the Azure AD Directory ID
        public static readonly string TenantID = "your AAD tenant here";

        static void Main()
        {
            Console.WriteLine("Starting up Fight Quote App...");

            var builder = new SqlConnectionStringBuilder();

            builder.ConnectionString = Utility.GetConnectionStringByName("fightdata");

            Console.Write("Token mode, no credentials needed here!");
            Console.WriteLine();

            Utility.GetConnectionStringInfo(builder);

            Utility.PrintBanner();
            
            Console.WriteLine(GetFightFactEF(builder.ConnectionString));

            Utility.PrintEnding();
        }

        static string GetFightFactEF(string connectionString)
        {
        string quote = "";
        SqlConnection connection = new SqlConnection(connectionString);
        
            var authority = string.Format("https://login.windows.net/{0}", TenantID);
            var resource = "https://database.windows.net/";
            var scope = "";
            connection.AccessToken =
                GetAccessTokenAsync(ClientApplicationID, ClientSecret, authority, resource, scope).GetAwaiter()
                    .GetResult();

            var efcon = GetEntityConnectionString(connection);

            using (var context = new ufcdataEntities(efcon))
            {
                var fightFact = context.get_random_fight_fact().First<get_random_fight_fact_Result>();
                quote = string.Format(
                    "On {0:MM/dd/yyyy}, {1} beat {2} in a {3} {4} in {5}. It was officiated by {6}.",
                    Convert.ToDateTime(fightFact.date),
                    fightFact.Winner,
                    fightFact.Loser,
                    fightFact.Format,
                    fightFact.Fight_type,
                    fightFact.location,
                    fightFact.Referee);
            }

        return quote;
        }

        public static async Task<string> GetAccessTokenAsync(string clientId, string clientSecret, string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority, TokenCache.DefaultShared);
            var clientCred = new ClientCredential(clientId, clientSecret);
            var result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
            {
                throw new InvalidOperationException("Could not get token");
            }

            return result.AccessToken;
        }

        public static EntityConnection GetEntityConnectionString(SqlConnection sqlConnection)
        {

            MetadataWorkspace workspace = new MetadataWorkspace(
                new string[] { "res://*/" },
                new Assembly[] { Assembly.GetExecutingAssembly() });

            EntityConnection entityConnection = new EntityConnection(workspace, sqlConnection);
            return entityConnection;
        }

    }
}
