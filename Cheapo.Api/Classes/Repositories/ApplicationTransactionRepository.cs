using Cheapo.Api.Data;
using Cheapo.Api.Interfaces.Repositories;

namespace Cheapo.Api.Classes.Repositories;

public class ApplicationTransactionRepository : BaseRepository, IApplicationTransactionRepository
{
    public ApplicationTransactionRepository(ApplicationDbContext context) : base(context)
    {
    }
}