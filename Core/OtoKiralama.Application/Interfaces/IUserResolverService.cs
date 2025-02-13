namespace OtoKiralama.Application.Interfaces
{
    public interface IUserResolverService
    {
        Task<string?> GetCurrentUserIdAsync();
    }
}
