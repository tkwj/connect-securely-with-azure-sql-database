//https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/connection-strings-and-configuration-files#retrieving-connection-strings-at-run-time

// Retrieves a connection string by name.
// Returns null if the name is not found.
static string GetConnectionStringByName(string name)
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