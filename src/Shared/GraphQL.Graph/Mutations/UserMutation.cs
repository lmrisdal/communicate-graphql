using GraphQL.Data.UserRepository;
using GraphQL.Graph.Types;
using GraphQL.Models;
using GraphQL.Types;

namespace GraphQL.Graph.Mutations
{
    public class UserMutation : ObjectGraphType
    {
        private readonly IUserRepository repo;


        public UserMutation(IUserRepository repo)
        {
            this.repo = repo;

            Field<UserType>(
                "addUser",
                arguments: new QueryArguments
                {
                    new QueryArgument<CreateUserType>(){ Name = "user" }
                },
                resolve: context =>
                {
                    return repo.Add(context.GetArgument<User>("user"));
                }
            );

            Field<UserType>(
                "updateUser",
                arguments: new QueryArguments
                {
                    new QueryArgument<UpdateUserType>(){ Name = "user" }
                },
                resolve: context =>
                {
                    return repo.Update(context.GetArgument<User>("user"));
                }
            );

            Field<BooleanGraphType>(
                "deleteUser",
                arguments: new QueryArguments
                {
                    new QueryArgument<StringGraphType>(){ Name = "userId" }
                },
                resolve: context =>
                {
                    return repo.Delete(context.GetArgument<string>("userId"));
                }
            );

            Field<BooleanGraphType>(
                "updatePassword",
                arguments: new QueryArguments
                {
                    new QueryArgument<UpdatePasswordType>(){ Name = "passwordChange" }
                },
                resolve: context =>
                {
                    return repo.UpdatePassword(context.GetArgument<PasswordChangeRequest>("passwordChange"));
                }
            );
        }
    }
}
