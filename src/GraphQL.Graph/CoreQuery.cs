using GraphQL.Graph.Queries;
using GraphQL.Types;

namespace GraphQL.Graph
{
    public class CoreQuery : ObjectGraphType
    {
        public CoreQuery()
        {
            Name = "Query";
            Field<UserQuery>("userQuery", resolve: context => new { });
            Field<TaskItemQuery>("taskQuery", resolve: context => new { });
        }
    }
}
