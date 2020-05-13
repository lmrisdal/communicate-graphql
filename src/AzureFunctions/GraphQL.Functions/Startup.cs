using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using GraphQL.Models;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Data.UserRepository;
using GraphQL.Functions.Extensions;
using System;
using GraphQL.Server;
using GraphQL.Data.TodoItemRepository;

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
            builder.Services.AddCosmosStore<TodoItem>(cosmosSettings);

            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<ITodoItemRepository, TodoItemRepository>();

            builder.Services.AddGraphQLServices();
            builder.Services.AddGraphQL()
               .AddSystemTextJson();
        }
    }
}
