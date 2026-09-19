
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Platform platform)
        {
            ArgumentNullException.ThrowIfNull(platform);

            _context.Platforms.Add(platform);
        }

        public void DeletePlatform(int id)
        {
            var platform = _context.Platforms
                .FirstOrDefault(p => p.Id == id);

            if (platform == null)
            {
                return;
            }

            _context.Platforms.Remove(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms
                .AsNoTracking()
                .ToList();
        }

        public Platform? GetPlatformById(int id)
        {
            return _context.Platforms
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }

        public void UpdatePlatform(Platform platform)
        {
            ArgumentNullException.ThrowIfNull(platform);

            _context.Platforms.Update(platform);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}

