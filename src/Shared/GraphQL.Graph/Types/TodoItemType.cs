using GraphQL.Data.UserRepository;
using GraphQL.Models;
using GraphQL.Types;

namespace GraphQL.Graph.Types
{
    public class TodoItemType : ObjectGraphType<TodoItem>
    {
        public TodoItemType(IUserRepository userRepo)
        {
            Name = nameof(TodoItemType);
            Field(c => c.Id, type: typeof(IdGraphType));
            Field(c => c.Title);
            Field(c => c.Description);
            Field(c => c.AssignedUserId);
            Field(c => c.Created);
            Field(c => c.LastModified);
            Field<UserType>("assignedUser", resolve: context => userRepo.Fetch(context.Source.AssignedUserId));
        }
    }


    public class TodoItemInputType : InputObjectGraphType<TodoItem>
    {
        public TodoItemInputType()
        {
            Name = nameof(TodoItemInputType);
            Field(c => c.Id, type: typeof(IdGraphType));
            Field(c => c.Title, nullable: true);
            Field(c => c.Description, nullable: true);
            Field(c => c.AssignedUserId, nullable: true);
        }
    }
}
