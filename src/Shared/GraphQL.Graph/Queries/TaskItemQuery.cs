using GraphQL.Data.TaskItemRepository;
using GraphQL.Graph.Types;
using GraphQL.Types;

namespace GraphQL.Graph.Queries
{
    public class TaskItemQuery : ObjectGraphType
    {
        private readonly ITaskItemRepository repo;

        public TaskItemQuery(ITaskItemRepository repo)
        {
            this.repo = repo;

            Field<ListGraphType<TaskItemType>>("tasks", description: "Gets all tasks from the database", resolve: context => repo.FetchAll());

            Field<TaskItemType>(
                name: "task",
                description: "Gets a single task based on ID",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: context => repo.Fetch(context.GetArgument<string>("id"))
            );
        }
    }
}
