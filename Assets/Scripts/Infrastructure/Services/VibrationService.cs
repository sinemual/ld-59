using Client.Data.Core;
//using Lofelt.NiceVibrations;
using MoreMountains.NiceVibrations;

namespace Client.Infrastructure.Services
{
    public class VibrationService
    {
        private SharedData _data;

        public VibrationService(SharedData data)
        {
            _data = data;
        }

        public void Vibrate(HapticTypes HapticType)
        {
            if (_data.SaveData.IsVibrationOn)
                MMVibrationManager.Haptic(HapticType);
        }
    }
}