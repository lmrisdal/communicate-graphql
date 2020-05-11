using GraphQL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GraphQL.Validation.Rules.OverlappingFieldsCanBeMerged;

namespace GraphQL.Data.TaskItemRepository
{
    public interface ITaskItemRepository : IRepository<TaskItem>
    {
        Task<IList<TaskItem>> FetchTasksForUser(string userId);
    }
}
