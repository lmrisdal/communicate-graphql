using Communicate.ErrorHandling.Rest.Enums;
using Communicate.ErrorHandling.Rest.Exceptions;
using Cosmonaut;
using Cosmonaut.Extensions;
using Cosmonaut.Response;
using GraphQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Data.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ICosmosStore<User> store;

        public UserRepository(ICosmosStore<User> store)
        {
            this.store = store;
        }

        public async Task<IList<User>> FetchAll()
        {
            return await store.Query().ToListAsync();
        }

        public async Task<User> Fetch(string id)
        {
            return await store.FindAsync(id);
        }

        public async Task<User> Add(User entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            return await store.AddAsync(entity);
        }

        public async Task<User> Update(User entity)
        {
            if (string.IsNullOrEmpty(entity.Password))
            {
                var current = await Fetch(entity.Id);
                entity.Password = current.Password;
            }
            CosmosResponse<User> response = await store.UpdateAsync(entity);
            if (!response.IsSuccess && response.CosmosOperationStatus == CosmosOperationStatus.ResourceNotFound)
            {
                throw new NotFoundException(ErrorCode.NotFoundErrorGeneric, "Resource not found");
            }
            return response;
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                await store.RemoveByIdAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePassword(PasswordChangeRequest request)
        {
            if (request.NewPassword != request.NewPasswordConfirm)
            {
                throw new BadRequestException(ErrorCode.ValidationErrorGeneric, "Confirm password does not match new password");
            }
            User current = await Fetch(request.UserId);
            if (request.CurrentPassword != current.Password)
            {
                throw new BadRequestException(ErrorCode.ValidationErrorGeneric, "Please enter the corrent current password");
            }
            if (request.NewPassword == current.Password)
            {
                return true;
            }
            current.Password = request.NewPassword;
            await Update(current);
            return true;
        }
    }
}
