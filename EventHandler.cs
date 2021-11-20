using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

namespace Zeprus.Sap {

    public interface IEvent {
        public void eventCalled(MethodInfo methodinfo);
    }

    public static class EventHandler {
        private static List<IEvent> subscribers = new List<IEvent>();

        public static void subscribe(IEvent subscriber) {
            subscribers.Add(subscriber);
        }

        public static void unsubscribe(IEvent subscriber) {
            subscribers.Remove(subscriber);
        }

        [HarmonyPatch(typeof(Spacewood.Unity.Lobby), "Awake")]
        class hookMenu {
            [HarmonyPrefix]
            public static void Prefix(ref Spacewood.Unity.Lobby __instance){
                foreach (IEvent subscriber in subscribers) {
                    subscriber.eventCalled(AccessTools.Method(typeof(Spacewood.Unity.Lobby), "Awake"));
                }
            }
        }
    }
}