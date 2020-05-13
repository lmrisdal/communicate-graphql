using GraphQL.Data.TodoItemRepository;
using GraphQL.Models;
using GraphQL.Types;

namespace GraphQL.Graph.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(ITodoItemRepository taskRepo)
        {
            Name = nameof(UserType);
            Field(u => u.Id, type: typeof(IdGraphType));
            Field(u => u.FirstName);
            Field(u => u.LastName);
            Field(u => u.Name);
            Field(u => u.Email);
            Field(u => u.PhoneNumber);
            Field(u => u.Password);
            Field<AddressType>("address", nameof(AddressType));
            Field<ListGraphType<TodoItemType>>("todos", resolve: context => taskRepo.FetchTodosForUser(context.Source.Id));
        }
    }

    public class CreateUserType : InputObjectGraphType<User>
    {
        public CreateUserType()
        {
            Name = nameof(CreateUserType);
            Field(c => c.Id, type: typeof(IdGraphType));
            Field(u => u.FirstName);
            Field(u => u.LastName);
            Field(u => u.Email);
            Field(u => u.PhoneNumber);
            Field(u => u.Password);
            Field<AddressInputType>("address", nameof(AddressInputType));
        }
    }

    public class UpdateUserType : InputObjectGraphType<User>
    {
        public UpdateUserType()
        {
            Name = nameof(UpdateUserType);
            Field(c => c.Id, type: typeof(IdGraphType));
            Field(u => u.FirstName);
            Field(u => u.LastName);
            Field(u => u.Email);
            Field(u => u.PhoneNumber);
            Field<AddressInputType>("address", nameof(AddressInputType));
        }
    }

    public class UpdatePasswordType : InputObjectGraphType<PasswordChangeRequest>
    {
        public UpdatePasswordType()
        {
            Name = nameof(UpdatePasswordType);
            Field(c => c.UserId, type: typeof(IdGraphType));
            Field(u => u.CurrentPassword);
            Field(u => u.NewPassword);
            Field(u => u.NewPasswordConfirm);
        }
    }

    public class AddressType : ObjectGraphType<Address>
    {
        public AddressType()
        {
            Name = nameof(AddressType);
            Field(a => a.Country);
            Field(a => a.Street);
            Field(a => a.City);
            Field(a => a.Zip);
        }
    }

    public class AddressInputType : InputObjectGraphType<Address>
    {
        public AddressInputType()
        {
            Name = nameof(AddressInputType);
            Field(a => a.Country);
            Field(a => a.Street);
            Field(a => a.City);
            Field(a => a.Zip);
        }
    }
}
