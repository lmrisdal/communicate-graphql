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

            Field<ListGraphType<TaskItemType>>("tasks", resolve: context => repo.FetchAll());

            Field<TaskItemType>(
                name: "task",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: context => repo.Fetch(context.GetArgument<string>("id"))
            );
        }
    }
}
