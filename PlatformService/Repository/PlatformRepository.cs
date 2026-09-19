using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        void IPlatformRepository.CreatePlatform(Platform platform)
        {
            throw new NotImplementedException();
        }

        void IPlatformRepository.DeletePlatform(Platform platform)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Platform> IPlatformRepository.GetAllPlatforms()
        {
            throw new NotImplementedException();
        }

        Platform? IPlatformRepository.GetPlatformById(int id)
        {
            throw new NotImplementedException();
        }

        bool IPlatformRepository.SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        void IPlatformRepository.UpdatePlatform(Platform platform)
        {
            throw new NotImplementedException();
        }
    }
}
