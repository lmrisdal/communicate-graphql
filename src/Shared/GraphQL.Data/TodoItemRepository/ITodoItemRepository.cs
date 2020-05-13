using GraphQL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GraphQL.Validation.Rules.OverlappingFieldsCanBeMerged;

namespace GraphQL.Data.TodoItemRepository
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        Task<IList<TodoItem>> FetchTodosForUser(string userId);
    }
}
