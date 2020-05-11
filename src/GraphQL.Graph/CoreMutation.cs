using GraphQL.Graph.Mutations;
using GraphQL.Types;

namespace GraphQL.Graph
{
    public class CoreMutation : ObjectGraphType
    {
        public CoreMutation()
        {
            Name = "Mutation";
            Field<UserMutation>("users", resolve: context => new { });
            Field<TaskItemMutation>("tasks", resolve: context => new { });
        }
    }
}
