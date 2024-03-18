using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using RimWorld.BaseGen;



namespace VFEProps
{


    [HarmonyPatch(typeof(Graphic_Cluster))]
    [HarmonyPatch("DrawWorker")]


    public static class VFEProps_Graphic_Cluster_DrawWorker_Patch
    {


        [HarmonyPrefix]
        public static bool DisableErrors(ThingDef thingDef)

        {
            if (StaticCollections.stupidErrors_Things.Contains(thingDef))
            {
                return false;
            }
            return true;




        }

    }


}