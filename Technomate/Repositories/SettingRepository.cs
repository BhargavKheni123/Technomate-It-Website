using System.Linq;
using Technomate.Models;
using Technomate.Repositories;

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

    public void UpdateSetting(Setting model)
    {
        // Get existing record
        var existing = _context.Setting.FirstOrDefault();

        if (existing == null)
        {
            // Table empty → create first record
            existing = new Setting();
            _context.Setting.Add(existing);
        }

        // Update fields
        existing.Email = model.Email;
        existing.Phone = model.Phone;
        existing.Website = model.Website;
        existing.Address = model.Address;
        existing.AboutTitle = model.AboutTitle;
        existing.AboutShortDescription = model.AboutShortDescription;
        existing.Point1 = model.Point1;
        existing.Point2 = model.Point2;
        existing.Point3 = model.Point3;
        existing.AboutFullDescription = model.AboutFullDescription;

        _context.SaveChanges(); // EF tracks existing object → no new record if exists
    }
}
