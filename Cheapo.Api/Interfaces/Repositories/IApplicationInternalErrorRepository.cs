using Cheapo.Api.Entities;

namespace Cheapo.Api.Interfaces.Repositories;

public interface IApplicationInternalErrorRepository
{
    /// <summary>
    ///     Save asynchronously a server internal error (500) into the database.
    /// </summary>
    /// <param name="error"><see cref="ApplicationInternalError" />.</param>
    public Task SaveErrorAsync(ApplicationInternalError error);
}