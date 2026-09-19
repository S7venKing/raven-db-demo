using PlatformService.Models;

namespace PlatformService.Repository
{
    public interface IPlatformRepository
    {
        bool SaveChanges();

        IEnumerable<Models.Platform> GetAllPlatforms();
        Platform? GetPlatformById(int id);
        void CreatePlatform(Platform platform);
        void UpdatePlatform(Platform platform);
        void DeletePlatform(int id);

    }
}
