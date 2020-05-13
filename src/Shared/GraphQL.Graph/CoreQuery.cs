using GraphQL.Graph.Queries;
using GraphQL.Types;

namespace GraphQL.Graph
{
    public class CoreQuery : ObjectGraphType
    {
        public CoreQuery()
        {
            Name = "Query";
            Field<UserQuery>("users", description: "Queries for users", resolve: context => new { });
            Field<TodoItemQuery>("todos", description: "Queries for todo items", resolve: context => new { });
        }
    }
}
