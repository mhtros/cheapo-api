using Cheapo.Api.Entities;

namespace Cheapo.Api.Interfaces.Repositories;

public interface IApplicationInternalErrorRepository : ISaveable
{
    /// <summary>
    ///     Adds a new item into the ApplicationInternalErrors table.
    /// </summary>
    public Task AddAsync(ApplicationInternalError entity);
}