namespace Cheapo.Api.Interfaces;

public interface IDistinctable<T>
{
    public T Id { get; set; }
}