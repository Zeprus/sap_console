using System;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

namespace zeprus.sap {
    public class Sandbox : MonoBehaviour {
        private static BepInEx.Logging.ManualLogSource log;
        MethodInfo startConsole = AccessTools.Method(typeof(Spacewood.Unity.Menu), "StartConsole");
        FieldInfo settingsButtonInfo = AccessTools.DeclaredField(typeof(Spacewood.Unity.Menu), "SettingsMenuButton");
        
        public Sandbox(IntPtr ptr) : base(ptr) {
            log = BepInExLoader.log;
        }

        public void hangarMainUpdatePostFix() {
            //todo: Implement stuff
        }

        class MenuButton : UnityEngine.UI.Button {
            public MenuButton(){
            }

            void clicked(){

            }
        }

        //Hook achievement button to attach additional functionality
        [HarmonyPatch(typeof(Spacewood.Unity.Menu), "StartAchievements")]
        class updatePatch{

            //Overwrite achievement button to open console
            [HarmonyPrefix]
            public static bool Prefix(ref Spacewood.Unity.Menu __instance){
                __instance.StartConsole();
                return false;
            }
        }
    }
}