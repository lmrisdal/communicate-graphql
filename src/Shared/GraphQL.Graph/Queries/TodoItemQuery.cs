using GraphQL.Data.TodoItemRepository;
using GraphQL.Graph.Types;
using GraphQL.Types;

namespace GraphQL.Graph.Queries
{
    public class TodoItemQuery : ObjectGraphType
    {
        private readonly ITodoItemRepository repo;

        public TodoItemQuery(ITodoItemRepository repo)
        {
            this.repo = repo;

            Field<ListGraphType<TodoItemType>>("allTodos", description: "Gets all todos from the database", resolve: context => repo.FetchAll());

            Field<TodoItemType>(
                name: "todo",
                description: "Gets a single todo based on ID",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: context => repo.Fetch(context.GetArgument<string>("id"))
            );
        }
    }
}
