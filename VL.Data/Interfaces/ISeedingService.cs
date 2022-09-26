namespace VL.Shared.Interfaces
{
    public interface ISeedingService
    {
        Task SeedBooksAsync(int count);
        Task SeedUsersAsync(int count);
    }
}
