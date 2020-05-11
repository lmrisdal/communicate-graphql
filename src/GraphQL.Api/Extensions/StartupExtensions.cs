using GraphQL.Graph;
using GraphQL.Graph.Mutations;
using GraphQL.Graph.Queries;
using GraphQL.Graph.Types;
using GraphQL.Models;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Api.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter>(sp => new DocumentExecuter());
            services.AddSingleton<CoreMutation>();
            services.AddSingleton<UserMutation>();
            services.AddSingleton<TaskItemMutation>();
            services.AddSingleton<CoreQuery>();
            services.AddSingleton<UserQuery>();
            services.AddSingleton<TaskItemQuery>();
            services.AddSingleton<UserType>();
            services.AddSingleton<AddressType>();
            services.AddSingleton<AddressInputType>();
            services.AddSingleton<CreateUserType>();
            services.AddSingleton<UpdateUserType>();
            services.AddSingleton<UpdatePasswordType>();
            services.AddSingleton<TaskItemType>();
            services.AddSingleton<TaskItemInputType>();
            services.AddSingleton<TaskItem>();
            services.AddSingleton<User>();

            services.AddSingleton<ISchema, CoreSchema>();

            return services;
        }
    }
}
