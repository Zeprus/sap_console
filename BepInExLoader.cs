using BepInEx;
using UnhollowerRuntimeLib;
using UnityEngine;
using HarmonyLib;

namespace zeprus.sap {
    [BepInPlugin("com.zeprus.sap_plugin", "SAP Plugin", "0.1")]
    [BepInProcess("Super Auto Pets.exe")]
    public class BepInExLoader : BepInEx.IL2CPP.BasePlugin {

        public const string
            MODNAME = "sap_plugin",
            AUTHOR = "zeprus",
            GUID = "com." + AUTHOR + "." + MODNAME,
            VERSION = "1";

        public static BepInEx.Logging.ManualLogSource log;
        public BepInExLoader() {
            log = Log;

            try{
                Harmony harmonyInstance = new Harmony("zeprus.sap_plugin");
                harmonyInstance.PatchAll();
            } catch(HarmonyException e) {
                log.LogError(e.ToString());
                log.LogError(e.StackTrace);
            }
        }

        public override void Load()
        {
            log.LogMessage(GUID + ": Registering...");
            
            try {
                var gameObject = new GameObject("Sandbox");

                ClassInjector.RegisterTypeInIl2Cpp<Sandbox>();
                gameObject.AddComponent<Sandbox>();

                ClassInjector.RegisterTypeInIl2Cpp<Console>();;
                gameObject.AddComponent<Console>();

                log.LogMessage(GUID + ": Finished registering.");
            } catch {
                log.LogError(GUID + ": Failed to register classes.");
            }
        }
    }
}