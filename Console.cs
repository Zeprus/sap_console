using System;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

namespace zeprus.sap {
    public class Console : MonoBehaviour {
        private static BepInEx.Logging.ManualLogSource log;
        MethodInfo startConsole = AccessTools.Method(typeof(Spacewood.Unity.Menu), "StartConsole");
        public static Spacewood.Unity.Menu menu;
        
        public Console(IntPtr ptr) : base(ptr) {
            log = BepInExLoader.log;
        }

        public void hangarMainUpdatePostFix() {

        }

        public void lobbyUpdatePostFix() {
            log.LogMessage("Updating..");
            if(Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.F1) && Event.current.type == EventType.KeyDown) {
                log.LogMessage("Registered button press");
            }
        }

        [HarmonyPatch(typeof(Spacewood.Unity.Menu), "Start")]
        class menuStart{
            [HarmonyPrefix]
            public static void Postfix(ref Spacewood.Unity.Menu __instance){
                Console.menu = __instance;
                __instance.StartConsole();
                log.LogMessage(BepInExLoader.GUID + ": Menu started.");
            }
        }
    }
}