using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

namespace VFEProps
{
    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.VFEProps");
            harmony.PatchAll(Assembly.GetExecutingAssembly());       
        }          
    }
}
