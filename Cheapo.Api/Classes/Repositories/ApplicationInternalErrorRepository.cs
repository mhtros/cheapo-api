using Cheapo.Api.Data;
using Cheapo.Api.Entities;
using Cheapo.Api.Interfaces.Repositories;

namespace Cheapo.Api.Classes.Repositories;

public class ApplicationInternalErrorRepository : IApplicationInternalErrorRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationInternalErrorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveErrorAsync(ApplicationInternalError error)
    {
        await _context.ApplicationInternalErrors.AddAsync(error);
        await _context.SaveChangesAsync();
    }
}