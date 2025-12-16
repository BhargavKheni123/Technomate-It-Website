using Technomate.Models;

namespace Technomate.Repositories
{
    public interface ISettingRepository
    {
        Setting GetSetting();
        void UpdateSetting(Setting setting);
    }

}
