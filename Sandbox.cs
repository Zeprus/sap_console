using System;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

namespace zeprus.sap {
    public class Sandbox : MonoBehaviour {
        private static BepInEx.Logging.ManualLogSource log;

        public Sandbox(IntPtr ptr) : base(ptr) {
            log = BepInExLoader.log;
        }

        [HarmonyPatch(typeof(Spacewood.Unity.Lobby), "Awake")]
        class hookMenu{

            //Overwrite achievement button to open console
            [HarmonyPrefix]
            public static bool Prefix(ref Spacewood.Unity.Lobby __instance){
                log.LogMessage(BepInExLoader.GUID + ": Lobby awake");
                return true;
            }
        }
    }
}