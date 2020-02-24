using System;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using FightQuoteLibrary;

namespace FightQuoteCloud
{
    class Program
    {
        //Now we need some Application level IDs
        public static readonly string ClientApplicationID = "your-appID-guid-here";
        public static readonly Uri RedirectUri = new Uri("https://login.microsoftonline.com/common/oauth2/nativeclient");

        static void Main()
        {
            Console.WriteLine("Starting up Fight Quote App...");

            var builder = new SqlConnectionStringBuilder();

            builder.ConnectionString = Utility.GetConnectionStringByName("fightdata");

            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            builder.UserID = username;

            builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryInteractive;

            var provider = new ActiveDirectoryAuthProvider();
            SqlAuthenticationProvider.SetProvider(SqlAuthenticationMethod.ActiveDirectoryInteractive, provider);

            Utility.GetConnectionStringInfo(builder);

            Utility.PrintBanner();

            Console.WriteLine(Utility.GetFightFact(builder.ConnectionString));

            Utility.PrintEnding();
        }

        public class ActiveDirectoryAuthProvider : SqlAuthenticationProvider
        {
            // Program._ more static values that you set!
            private readonly string _clientId = Program.ClientApplicationID;
            private readonly Uri _redirectUri = Program.RedirectUri;

            public override async Task<SqlAuthenticationToken>
                AcquireTokenAsync(SqlAuthenticationParameters parameters)
            {
                AuthenticationContext authContext =
                    new AuthenticationContext(parameters.Authority);
                authContext.CorrelationId = parameters.ConnectionId;

                var result = await authContext.AcquireTokenAsync(
                    parameters.Resource,  // "https://database.windows.net/"
                    _clientId,
                    _redirectUri,
                    new PlatformParameters(PromptBehavior.Auto),
                    new UserIdentifier(
                        parameters.UserId,
                        UserIdentifierType.RequiredDisplayableId));

                return new SqlAuthenticationToken(result.AccessToken, result.ExpiresOn);
            }

            public override bool IsSupported(SqlAuthenticationMethod authenticationMethod)
            {
                return authenticationMethod == SqlAuthenticationMethod.ActiveDirectoryIntegrated
                    || authenticationMethod == SqlAuthenticationMethod.ActiveDirectoryInteractive
                    || authenticationMethod == SqlAuthenticationMethod.ActiveDirectoryPassword;
            }
        }
    }
}
