using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

using GP_Utilities.Console;

namespace GamePush
{
    public class GP_Language : MonoBehaviour
    {
        public static event UnityAction<string> OnChangeLanguage;
        public static event Action<string> _onChangeLanguage;

        [DllImport("__Internal")]
        private static extern string GP_Current_Language();
        public static string Current()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            return GP_Current_Language();
#else
            if (GP_ConsoleController.Instance.LanguageConsoleLogs)
                Console.Log("LANGUAGE CURRENT: ", "en");
            return "en";
#endif
        }


        [DllImport("__Internal")]
        private static extern void GP_ChangeLanguage(string language);
        public static void Change(Language language, Action<string> onLanguageChange = null)
        {
            _onChangeLanguage = onLanguageChange;
#if !UNITY_EDITOR && UNITY_WEBGL
            GP_ChangeLanguage(language.ToString());
#else
            if (GP_ConsoleController.Instance.LanguageConsoleLogs)
                Console.Log("LANGUAGE CHANGE: ", language.ToString());
            OnChangeLanguage?.Invoke(language.ToString());
            _onChangeLanguage?.Invoke(language.ToString());
#endif
        }

        private void CallChangeLanguage(string language) { _onChangeLanguage?.Invoke(language); OnChangeLanguage?.Invoke(language); }
    }
    public enum Language : byte
    {
        /* English */
        en,

        /* Russian */
        ru,

        /* Turkish */
        tr,

        /* French */
        fr,

        /* Italian */
        it,

        /* German */
        de,

        /* Spanish */
        es,

        /* Chineese */
        zh,

        /* Portuguese */
        pt,

        /* Korean */
        ko,

        /* Japanese */
        ja,

        /* Arabian */
        ar,

        /* Hindi */
        hi,

        /* Indonesian */
        id,
    }
}