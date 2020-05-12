using GraphQL.Data.UserRepository;
using GraphQL.Models;
using GraphQL.Types;

namespace GraphQL.Graph.Types
{
    public class TaskItemType : ObjectGraphType<TaskItem>
    {
        public TaskItemType(IUserRepository userRepo)
        {
            Name = "TaskItemType";
            Field(c => c.Id, type: typeof(IdGraphType));
            Field(c => c.Title);
            Field(c => c.Description);
            Field(c => c.AssignedUserId);
            Field(c => c.Created);
            Field(c => c.LastModified);
            Field<UserType>("assignedUser", resolve: context => userRepo.Fetch(context.Source.AssignedUserId));
        }
    }


    public class TaskItemInputType : InputObjectGraphType<TaskItem>
    {
        public TaskItemInputType()
        {
            Name = "TaskItemInputType";
            Field(c => c.Id, type: typeof(IdGraphType));
            Field(c => c.Title, nullable: true);
            Field(c => c.Description, nullable: true);
            Field(c => c.AssignedUserId, nullable: true);
        }
    }
}
