using Cheapo.Api.Data;
using Cheapo.Api.Entities;
using Cheapo.Api.Interfaces.Repositories;

namespace Cheapo.Api.Classes.Repositories;

public class ApplicationInternalErrorRepository : BaseRepository, IApplicationInternalErrorRepository
{
    public ApplicationInternalErrorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task AddAsync(ApplicationInternalError entity)
    {
        await Context.ApplicationInternalErrors.AddAsync(entity);
    }
}