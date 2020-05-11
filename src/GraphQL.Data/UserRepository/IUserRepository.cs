using GraphQL.Models;
using System.Threading.Tasks;

namespace GraphQL.Data.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> UpdatePassword(PasswordChangeRequest request);
    }
}
