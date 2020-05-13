using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using GraphQL.Data.TodoItemRepository;
using GraphQL.Data.UserRepository;
using GraphQL.Models;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQL.Types;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;
using GraphQL.Api.Middleware;
using GraphQL.Api.Extensions;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Voyager;

namespace GraphQL.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string databaseName = Configuration.GetSection("CosmosSettings:DatabaseName").Value;
            string databaseUri = Configuration.GetSection("CosmosSettings:DatabaseUri").Value;
            string authKey = Configuration.GetSection("CosmosSettings:DatabaseKey").Value;

            CosmosStoreSettings cosmosSettings = new CosmosStoreSettings(databaseName, databaseUri, authKey);
            services.AddCosmosStore<User>(cosmosSettings);
            services.AddCosmosStore<TodoItem>(cosmosSettings);

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITodoItemRepository, TodoItemRepository>();

            services.AddGraphQLServices();
            services.AddGraphQL()
               .AddSystemTextJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "REST API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseGraphQL<ISchema>("/graphql");

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseGraphiQLServer(new GraphiQLOptions { Path = "/ui/graphiql" });
            app.UseGraphQLVoyager(new GraphQLVoyagerOptions());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "REST API");
            });

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
