using System;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

namespace Zeprus.Sap {
    public class Sandbox : MonoBehaviour, IEvent{
        private static BepInEx.Logging.ManualLogSource log;

        public Sandbox(IntPtr ptr) : base(ptr) {
            log = BepInExLoader.log;
            EventHandler.subscribe(this);
        }

        void lobbyAwake() {
            log.LogMessage("Called lobbyAwake in Sandbox");
        }

        public void eventCalled(MethodInfo methodInfo){
            if(methodInfo == AccessTools.Method(typeof(Spacewood.Unity.Lobby), "Awake")) {
                lobbyAwake();
            }
        }
    }
}