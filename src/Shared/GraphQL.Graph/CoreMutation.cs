using GraphQL.Graph.Mutations;
using GraphQL.Types;

namespace GraphQL.Graph
{
    public class CoreMutation : ObjectGraphType
    {
        public CoreMutation()
        {
            Name = "Mutation";
            Field<UserMutation>("users", description: "Mutations for users", resolve: context => new { });
            Field<TaskItemMutation>("tasks", description: "Mutations for tasks", resolve: context => new { });
        }
    }
}
