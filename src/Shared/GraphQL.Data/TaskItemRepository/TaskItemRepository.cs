using Communicate.ErrorHandling.Rest.Enums;
using Communicate.ErrorHandling.Rest.Exceptions;
using Cosmonaut;
using Cosmonaut.Extensions;
using GraphQL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL.Data.TaskItemRepository
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ICosmosStore<TaskItem> store;

        public TaskItemRepository(ICosmosStore<TaskItem> store)
        {
            this.store = store;
        }

        public async Task<IList<TaskItem>> FetchAll()
        {
            return await store.Query().ToListAsync();
        }

        public async Task<TaskItem> Fetch(string id)
        {
            return await store.FindAsync(id);
        }

        public async Task<IList<TaskItem>> FetchTasksForUser(string userId)
        {
            var tasks = await store.Query($"select * from c where c.assignedUserId = '{userId}'").ToListAsync();
            return tasks;
        }

        public async Task<TaskItem> Add(TaskItem entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.Created = DateTime.UtcNow;
            entity.LastModified = entity.Created;
            return await store.AddAsync(entity);
        }

        public async Task<TaskItem> Update(TaskItem entity)
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

        private async Task<TaskItem> CompareObjectForUpdate(TaskItem entity)
        {

            TaskItem current = null;
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
