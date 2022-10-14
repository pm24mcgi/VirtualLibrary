namespace VL.Shared.Interfaces
{
    public interface IMigrateService
    {
        Task<bool> HasRolesAsync();
    }
}
