using Communicate.ErrorHandling.Rest.Enums;
using Communicate.ErrorHandling.Rest.Exceptions;
using Cosmonaut;
using Cosmonaut.Extensions;
using GraphQL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL.Data.TodoItemRepository
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly ICosmosStore<TodoItem> store;

        public TodoItemRepository(ICosmosStore<TodoItem> store)
        {
            this.store = store;
        }

        public async Task<IList<TodoItem>> FetchAll()
        {
            return await store.Query().ToListAsync();
        }

        public async Task<TodoItem> Fetch(string id)
        {
            return await store.FindAsync(id);
        }

        public async Task<IList<TodoItem>> FetchTodosForUser(string userId)
        {
            var todos = await store.Query($"select * from c where c.assignedUserId = '{userId}'").ToListAsync();
            return todos;
        }

        public async Task<TodoItem> Add(TodoItem entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.Created = DateTime.UtcNow;
            entity.LastModified = entity.Created;
            return await store.AddAsync(entity);
        }

        public async Task<TodoItem> Update(TodoItem entity)
        {
            if (string.IsNullOrEmpty(entity.Title) && string.IsNullOrEmpty(entity.Description) && string.IsNullOrEmpty(entity.AssignedUserId))
            {
                throw new BadRequestException(ErrorCode.ValidationErrorGeneric);
            }
            entity = await CompareObjectForUpdate(entity);
            entity.LastModified = DateTime.UtcNow;
            return await store.UpdateAsync(entity);
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

        private async Task<TodoItem> CompareObjectForUpdate(TodoItem entity)
        {

            TodoItem current = null;
            if (entity.Created == null)
            {
                if (current == null)
                    current = await Fetch(entity.Id);
                entity.Created = current.Created;
            }
            if (string.IsNullOrEmpty(entity.Description))
            {
                if (current == null)
                    current = await Fetch(entity.Id);
                entity.Description = current.Description;
            }
            if (string.IsNullOrEmpty(entity.Title))
            {
                if (current == null)
                    current = await Fetch(entity.Id);
                entity.Title = current.Title;
            }
            if (string.IsNullOrEmpty(entity.AssignedUserId))
            {
                if (current == null)
                    current = await Fetch(entity.Id);
                entity.AssignedUserId = current.AssignedUserId;
            }
            return entity;
        }
    }
}
