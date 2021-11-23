using System;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

namespace Zeprus.Sap {
    public class Console : MonoBehaviour, IEvent {
        private static BepInEx.Logging.ManualLogSource log;
        public static Spacewood.Unity.Menu menu;
        public static Spacewood.Unity.Lobby lobby;
        public static Il2CppSystem.Action<Spacewood.Unity.UI.SelectableBase> achievementButtonSubmitAction;
        
        public Console(IntPtr ptr) : base(ptr) {
            log = BepInExLoader.log;
            EventHandler.subscribe(this);

            achievementButtonSubmitAction = new Action<Spacewood.Unity.UI.SelectableBase>((selectableBase) => {
                achievementOnSubmit(selectableBase);
            });
        }

        void menuStart(Spacewood.Unity.Menu __instance){
            Console.menu = __instance;
            Console.lobby = __instance.Lobby;
            Console.lobby.AchievementsButton.OnSubmit = achievementButtonSubmitAction;
        }

        public static void achievementOnSubmit(Spacewood.Unity.UI.SelectableBase button) {
            if(Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.LeftControl)) {
                menu.StartConsole();
            }   else {
                menu.StartAchievements();
            }
        }

        public void eventCalled(MethodInfo methodInfo, ref object __instance) {
            if(methodInfo == AccessTools.Method(typeof(Spacewood.Unity.Menu), "Start")) {
                menuStart((Spacewood.Unity.Menu) __instance);
                return;
            }
        }
    }
}