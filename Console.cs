using System;
using UnityEngine;
using UnityEngine.EventSystems;
using HarmonyLib;
using System.Reflection;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

namespace Zeprus.Sap {
    public class Console : MonoBehaviour, IEvent {
        private static BepInEx.Logging.ManualLogSource log;
        public static Spacewood.Unity.Menu menu;
        public static Spacewood.Unity.Lobby lobby;
        public static Il2CppSystem.Action<Spacewood.Unity.UI.SelectableBase> achievementPressedAction;
        
        public Console(IntPtr ptr) : base(ptr) {
            log = BepInExLoader.log;
            EventHandler.subscribe(this);
            achievementPressedAction = new Action<Spacewood.Unity.UI.SelectableBase>((selectableBase) => {
                achievementOnClick(selectableBase);
            });
        }

        public void hangarMainUpdatePostFix() {

        }

        void lobbyAwake() {
            log.LogMessage("Called lobbyAwake in Console");
        }

        public static void achievementOnClick(Spacewood.Unity.UI.SelectableBase button) {
            if(Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.LeftControl)) {
                menu.StartConsole();
            }   else {
                menu.StartAchievements();
            }
        }

        public void eventCalled(MethodInfo methodInfo) {
            if(methodInfo == AccessTools.Method(typeof(Spacewood.Unity.Lobby), "Awake")) {
                lobbyAwake();
            }
        }

        [HarmonyPatch(typeof(Spacewood.Unity.Menu), "Start")]
        class menuStart{
            [HarmonyPrefix]
            public static void Postfix(ref Spacewood.Unity.Menu __instance){
                log.LogMessage("Start");
                Console.menu = __instance;
                Console.lobby = __instance.Lobby;
                Console.lobby.AchievementsButton.OnSubmit = achievementPressedAction;
            }
        }
    }
}