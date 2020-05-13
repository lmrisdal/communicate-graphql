using GraphQL.Data.TodoItemRepository;
using GraphQL.Graph.Types;
using GraphQL.Models;
using GraphQL.Types;

namespace GraphQL.Graph.Mutations
{
    public class TodoItemMutation : ObjectGraphType
    {
        private readonly ITodoItemRepository repo;


        public TodoItemMutation(ITodoItemRepository repo)
        {
            this.repo = repo;

            Field<TodoItemType>(
                "addTodo",
                description: "Adds a todo to the database",
                arguments: new QueryArguments
                {
                    new QueryArgument<TodoItemInputType>(){ Name = "todo" }
                },
                resolve: context =>
                {
                    var task = context.GetArgument<TodoItem>("todo");
                    return repo.Add(task);
                }
            );

            Field<TodoItemType>(
                "updateTodo",
                description: "Updates a todo in the database",
                arguments: new QueryArguments
                {
                    new QueryArgument<TodoItemInputType>(){ Name = "todo" }
                },
                resolve: context =>
                {
                    return repo.Update(context.GetArgument<TodoItem>("todo"));
                }
            );

            Field<BooleanGraphType>(
                "deleteTodo",
                description: "Deletes a todo from the database",
                arguments: new QueryArguments
                {
                    new QueryArgument<StringGraphType>(){ Name = "todoId" }
                },
                resolve: context =>
                {
                    return repo.Delete(context.GetArgument<string>("todoId"));
                }
            );
        }
    }
}
