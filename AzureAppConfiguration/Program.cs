using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

Console.WriteLine("Azure App Configuration Article Demo\n");

//Retrieve the Connection String from Azure App Configuration Resource
const string connectionString = "{your_app_configuration_connection_string}";

var configuration = new ConfigurationBuilder()
            .AddAzureAppConfiguration(options =>
            {
                options.Connect(connectionString).UseFeatureFlags();
            }).Build();

var services = new ServiceCollection();

services.AddSingleton<IConfiguration>(configuration).AddFeatureManagement();

using (var serviceProvider = services.BuildServiceProvider())
{
    var featureManager = serviceProvider.GetRequiredService<IFeatureManager>();

    //read great day feature
    if (await featureManager.IsEnabledAsync("greatdayinfo"))
    {
        Console.WriteLine("Have a great day!!!");
    }
    else
    {
        Console.WriteLine("Please enable great day feature!");
    }
}