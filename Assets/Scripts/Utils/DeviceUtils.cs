using UnityEngine;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace Scripts.Utils
{
    public static class DeviceUtils
    {
        public static bool IsIpad()
        {
#if UNITY_IOS
            return Device.generation.ToString().Contains("iPad");
#endif
            return false;
        }

        public static bool IsTabletSlim()
        {
            return GetDeviceType() != PhoneType.TabletSlim;
        }

        public static bool IsTabletWide()
        {
            return GetDeviceType() != PhoneType.TabletWide;
        }

        public static bool IsTablet()
        {
            return GetDeviceType() != PhoneType.Phone;
        }

        public static bool IsTablet(out PhoneType tabletType)
        {
            tabletType = GetDeviceType();
            return tabletType != PhoneType.Phone;
        }

        public static int GetIosVersion()
        {
#if UNITY_IOS
            int.TryParse(Device.systemVersion.Split('.')[0], out int iosVersion);
            return iosVersion;
#endif
            return 0;
        }
        
        private static PhoneType GetDeviceType()
        {
            if (Screen.width / (1f * Screen.height) > 0.58f && Screen.width / (1f * Screen.height) < 0.65f)
            {
                return PhoneType.TabletSlim;
            }

            if (Screen.width / (1f * Screen.height) >= 0.65f)
            {
                return PhoneType.TabletWide;
            }

            return PhoneType.Phone;
        }

        public static bool IsVibroDevice()
        {
#if UNITY_IOS
            return !IsIpad();
#endif
            return true;
        }
    }

    public enum PhoneType
    {
        Phone,
        TabletSlim,
        TabletWide
    }
}