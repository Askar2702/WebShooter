using System.Runtime.InteropServices;
using UnityEngine;

using GP_Utilities.Console;

namespace GamePush
{
    public class GP_Platform : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern string GP_Platform_Type();
        public static string Type()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            return GP_Platform_Type();
#else
            if (GP_ConsoleController.Instance.PlatformConsoleLogs)
                Console.Log("PLATFORM: TYPE: ", "UNITY_DEMO");
            return "UNITY_DEMO";
#endif
        }


        [DllImport("__Internal")]
        private static extern string GP_Platform_HasIntegratedAuth();
        public static bool HasIntegratedAuth()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            return GP_Platform_HasIntegratedAuth() == "true";
#else
            if (GP_ConsoleController.Instance.PlatformConsoleLogs)
                Console.Log("PLATFORM: HAS INTEGRATED AUTH: ", "TRUE");
            return true;
#endif
        }


        [DllImport("__Internal")]
        private static extern string GP_Platform_IsExternalLinksAllowed();
        public static bool IsExternalLinksAllowed()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            return GP_Platform_IsExternalLinksAllowed() == "true";
#else
            if (GP_ConsoleController.Instance.PlatformConsoleLogs)
                Console.Log("PLATFORM: IS EXTERNAL LINKS ALLOWED: ", "TRUE");
            return true;
#endif
        }
    }

    public static class Platform
    {
        public const string YANDEX = "YANDEX";
        public const string VK = "VK";
        public const string CRAZY_GAMES = "CRAZY_GAMES";
        public const string GAME_DISTRIBUTION = "GAME_DISTRIBUTION";
        public const string GAME_MONETIZE = "GAME_MONETIZE";
        public const string OK = "OK";
        public const string SMARTMARKET = "SMARTMARKET";
        public const string GAMEPIX = "GAMEPIX";
        public const string POKI = "POKI";
        public const string VK_PLAY = "VK_PLAY";
    }
}

