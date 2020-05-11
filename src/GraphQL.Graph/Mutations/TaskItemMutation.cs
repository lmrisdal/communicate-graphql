using GraphQL.Data.TaskItemRepository;
using GraphQL.Graph.Types;
using GraphQL.Models;
using GraphQL.Types;

namespace GraphQL.Graph.Mutations
{
    public class TaskItemMutation : ObjectGraphType
    {
        private readonly ITaskItemRepository repo;


        public TaskItemMutation(ITaskItemRepository repo)
        {
            this.repo = repo;

            Field<TaskItemType>(
                "addTask",
                arguments: new QueryArguments
                {
                    new QueryArgument<TaskItemInputType>(){ Name = "task" }
                },
                resolve: context =>
                {
                    var task = context.GetArgument<TaskItem>("task");
                    return repo.Add(task);
                }
            );

            Field<TaskItemType>(
                "updateTask",
                arguments: new QueryArguments
                {
                    new QueryArgument<TaskItemInputType>(){ Name = "task" }
                },
                resolve: context =>
                {
                    return repo.Update(context.GetArgument<TaskItem>("task"));
                }
            );

            Field<BooleanGraphType>(
                "deleteTask",
                arguments: new QueryArguments
                {
                    new QueryArgument<StringGraphType>(){ Name = "taskId" }
                },
                resolve: context =>
                {
                    return repo.Delete(context.GetArgument<string>("taskId"));
                }
            );
        }
    }
}
