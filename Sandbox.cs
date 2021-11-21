using System;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

namespace Zeprus.Sap {
    public class Sandbox : MonoBehaviour, IEvent{
        private static BepInEx.Logging.ManualLogSource log;
        public static Spacewood.Unity.Menu menu;
        public static Spacewood.Unity.Lobby lobby;

        public static Il2CppSystem.Action<Spacewood.Unity.UI.SelectableBase> customizeButtonSubmitAction;


        public Sandbox(IntPtr ptr) : base(ptr) {
            log = BepInExLoader.log;
            EventHandler.subscribe(this);

            customizeButtonSubmitAction = new Action<Spacewood.Unity.UI.SelectableBase>((selectableBase) => {
                customizeOnSubmit(selectableBase);
            });
        }

        void menuStart(Spacewood.Unity.Menu __instance) {
            Console.menu = __instance;
            Console.lobby = __instance.Lobby;
            Console.lobby.CustomizeButton.OnSubmit = customizeButtonSubmitAction;
        }

        void customizeOnSubmit(Spacewood.Unity.UI.SelectableBase button) {
            if(Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.LeftControl)) {
                log.LogMessage("Ctrl-clicked customize button!");
                //TODO: Start sandbox
            }   else {
                menu.StartCustomize();
            }
        }

        public void eventCalled(MethodInfo methodInfo, object __instance){
            if(methodInfo == AccessTools.Method(typeof(Spacewood.Unity.Menu), "Start")) {
                menuStart((Spacewood.Unity.Menu) __instance);
                return;
            }
        }
    }
}