using GraphQL.Graph;
using GraphQL.Graph.Mutations;
using GraphQL.Graph.Queries;
using GraphQL.Graph.Types;
using GraphQL.Models;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Functions.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter>(sp => new DocumentExecuter());
            services.AddSingleton<CoreMutation>();
            services.AddSingleton<UserMutation>();
            services.AddSingleton<TodoItemMutation>();
            services.AddSingleton<CoreQuery>();
            services.AddSingleton<UserQuery>();
            services.AddSingleton<TodoItemQuery>();
            services.AddSingleton<UserType>();
            services.AddSingleton<AddressType>();
            services.AddSingleton<AddressInputType>();
            services.AddSingleton<CreateUserType>();
            services.AddSingleton<UpdateUserType>();
            services.AddSingleton<UpdatePasswordType>();
            services.AddSingleton<TodoItemType>();
            services.AddSingleton<TodoItemInputType>();
            services.AddSingleton<TodoItem>();
            services.AddSingleton<User>();

            services.AddSingleton<ISchema, CoreSchema>();

            return services;
        }
    }
}
