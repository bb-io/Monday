using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.Extensions.Configuration;

namespace Tests.Monday.Base;

public class TestBase
{
    protected TestBase()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        Creds = config.GetSection("ConnectionDefinition").GetChildren()
            .Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value!)).ToList();
        var folderLocation = config.GetSection("TestFolder").Value!;

        var variables = config.GetSection("Variables").GetChildren().ToList();
        BoardId = variables.FirstOrDefault(x => x.Key == "board_id")?.Value ??
                  throw new Exception("Couldn't find board id");
        ItemId = variables.FirstOrDefault(x => x.Key == "item_id")?.Value ??
                  throw new Exception("Couldn't find item id");

        InvocationContext = new InvocationContext
        {
            AuthenticationCredentialsProviders = Creds,
        };

        FileManager = new FileManager(folderLocation);
    }

    protected IEnumerable<AuthenticationCredentialsProvider> Creds { get; set; }
    
    protected string BoardId { get; init;  }
    
    protected string ItemId { get; init;  }

    public InvocationContext InvocationContext { get; set; }

    public FileManager FileManager { get; set; }
}