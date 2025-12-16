using Technomate.Models;

namespace Technomate.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly ApplicationDbContext _context;

        public SettingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Setting GetSetting()
        {
            return _context.Setting.FirstOrDefault();
        }

        public void UpdateSetting(Setting setting)
        {
            _context.Setting.Update(setting);
            _context.SaveChanges();
        }
    }

}
