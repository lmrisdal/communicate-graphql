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

            Field<ListGraphType<UserType>>("users", resolve: context => repo.FetchAll());

            Field<UserType>(
                name: "user",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: context => repo.Fetch(context.GetArgument<string>("id"))
            );
        }
    }
}
