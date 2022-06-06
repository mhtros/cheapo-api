namespace Cheapo.Api.Interfaces;

public interface ISaveable
{
    public Task<bool> SaveAsync();
}