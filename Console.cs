using System;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

namespace Zeprus.Sap {
    public class Console : MonoBehaviour, IEvent {
        private static BepInEx.Logging.ManualLogSource log;
        MethodInfo startConsole = AccessTools.Method(typeof(Spacewood.Unity.Menu), "StartConsole");
        public static Spacewood.Unity.Menu menu;
        public static Spacewood.Unity.Lobby lobby;
        
        public Console(IntPtr ptr) : base(ptr) {
            log = BepInExLoader.log;
            EventHandler.subscribe(this);
        }

        public void hangarMainUpdatePostFix() {

        }

        void lobbyAwake() {
            log.LogMessage("Called lobbyAwake in Console");
        }

        public void eventCalled(MethodInfo methodInfo) {
            if(methodInfo == AccessTools.Method(typeof(Spacewood.Unity.Lobby), "Awake")) {
                lobbyAwake();
            }
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
                log.LogMessage("Start");
                Console.menu = __instance;
                //Console.lobby = __instance.Lobby;
            }
        }
    }
}