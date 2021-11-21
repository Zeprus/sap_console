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
        public static Spacewood.Unity.PlayBox playbox;

        public static Il2CppSystem.Action<Spacewood.Unity.UI.SelectableBase> playButtonSubmitAction;
        public static Il2CppSystem.Action<Spacewood.Unity.UI.SelectableBase> continueButtonSubmitAction;


        public Sandbox(IntPtr ptr) : base(ptr) {
            log = BepInExLoader.log;
            EventHandler.subscribe(this);

            playButtonSubmitAction = new Action<Spacewood.Unity.UI.SelectableBase>((selectableBase) => {
                playOnSubmit(selectableBase);
            });
            continueButtonSubmitAction = new Action<Spacewood.Unity.UI.SelectableBase>((selectableBase) => {
                continueOnSubmit(selectableBase);
            });
        }

        void menuStart(Spacewood.Unity.Menu __instance) {
            Sandbox.menu = __instance;
            Sandbox.lobby = __instance.Lobby;
            Sandbox.playbox = lobby.playBox;
            Sandbox.playbox.PlayButton.OnSubmit = playButtonSubmitAction;
            Sandbox.playbox.ContinueButton.OnSubmit = continueButtonSubmitAction;
        }

        void playOnSubmit(Spacewood.Unity.UI.SelectableBase button) {
            if(Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.LeftControl)) {
                startSandbox();
            }   else {
                playbox.HandlePlay(button);
            }
        }
        void continueOnSubmit(Spacewood.Unity.UI.SelectableBase button) {
            if(Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.LeftControl)) {
                startSandbox();
            }   else {
                playbox.HandleContinue(button);
            }
        }

        void startSandbox() {
            log.LogMessage("This should start the sandbox at some point...");
            //TODO: start Sandbox
        }

        public void eventCalled(MethodInfo methodInfo, ref object __instance){
            if(methodInfo == AccessTools.Method(typeof(Spacewood.Unity.Menu), "Start")) {
                menuStart((Spacewood.Unity.Menu) __instance);
                return;
            }
        }
    }
}