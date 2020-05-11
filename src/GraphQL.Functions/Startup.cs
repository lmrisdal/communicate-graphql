using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using GraphQL.Models;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Data.TaskItemRepository;
using GraphQL.Data.UserRepository;
using GraphQL.Functions.Extensions;
using System;
using GraphQL.Server;

[assembly: FunctionsStartup(typeof(GraphQL.Functions.Startup))]
namespace GraphQL.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string? databaseName = Environment.GetEnvironmentVariable("DatabaseName");
            string? databaseUri = Environment.GetEnvironmentVariable("DatabaseUri");
            string? authKey = Environment.GetEnvironmentVariable("DatabaseKey");

            CosmosStoreSettings cosmosSettings = new CosmosStoreSettings(databaseName, databaseUri, authKey);
            builder.Services.AddCosmosStore<User>(cosmosSettings);
            builder.Services.AddCosmosStore<TaskItem>(cosmosSettings);

            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<ITaskItemRepository, TaskItemRepository>();

            builder.Services.AddGraphQLServices();
            builder.Services.AddGraphQL()
               .AddSystemTextJson();
        }
    }
}
