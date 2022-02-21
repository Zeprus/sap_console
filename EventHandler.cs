using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

namespace Zeprus.Sap {

    public interface IEvent {
        public void eventCalled(MethodInfo methodinfo, ref object __instance);
    }

    public static class EventHandler {
        private static List<IEvent> subscribers = new List<IEvent>();

        public static void subscribe(IEvent subscriber) {
            subscribers.Add(subscriber);
        }

        public static void unsubscribe(IEvent subscriber) {
            subscribers.Remove(subscriber);
        }

        [HarmonyPatch(typeof(Spacewood.Unity.Menu), "Start")]
        class menuStart{
            [HarmonyPostfix]
            public static void Postfix(ref object __instance){
                foreach (IEvent subscriber in subscribers) {
                    subscriber.eventCalled(AccessTools.Method(typeof(Spacewood.Unity.Menu), "Start"), ref __instance);
                }
            }
        }
    }
}