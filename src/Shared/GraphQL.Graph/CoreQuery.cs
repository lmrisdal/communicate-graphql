using GraphQL.Graph.Queries;
using GraphQL.Types;

namespace GraphQL.Graph
{
    public class CoreQuery : ObjectGraphType
    {
        public CoreQuery()
        {
            Name = "Query";
            Field<UserQuery>("userQuery", description: "Queries for users", resolve: context => new { });
            Field<TaskItemQuery>("taskQuery", description: "Queries for tasks", resolve: context => new { });
        }
    }
}
