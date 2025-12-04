using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Screw
{
    public static class EditorMenu
    {

        [MenuItem("Game/Clear Player Prefs", false, 11)]
        public static void ClearPlayerPref()
        {
            PlayerPrefs.DeleteAll();
            IPlayerPrefs.DeleteAll();
        }

        [MenuItem("Game/Enable Run in Background", false, 22)]
        public static void EnableRunInBG()
        {
            Application.runInBackground = true;
        }

        [MenuItem("Game/Enable Run in Background", true, 22)]
        public static bool EnableRunInBGConditional()
        {
            return !Application.runInBackground;
        }

        [MenuItem("Game/Disable Run in Background", false, 22)]
        public static void DisableRunInBG()
        {
            Application.runInBackground = false;
        }

        [MenuItem("Game/Disable Run in Background", true, 22)]
        public static bool DisableRunInBGConditional()
        {
            return Application.runInBackground;
        }
    }

}
