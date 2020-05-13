using GraphQL.Data.UserRepository;
using GraphQL.Graph.Types;
using GraphQL.Types;

namespace GraphQL.Graph.Queries
{
    public class UserQuery : ObjectGraphType
    {
        private readonly IUserRepository repo;

        public UserQuery(IUserRepository repo)
        {
            this.repo = repo;

            Field<ListGraphType<UserType>>("allUsers", description: "Gets all users from the database", resolve: context => repo.FetchAll());

            Field<UserType>(
                name: "user",
                description: "Gets a single user based on ID",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: context => repo.Fetch(context.GetArgument<string>("id"))
            );
        }
    }
}
