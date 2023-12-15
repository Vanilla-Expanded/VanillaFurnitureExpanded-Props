using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using RimWorld.BaseGen;



namespace VFEProps
{


    [HarmonyPatch(typeof(ThingUtility))]
    [HarmonyPatch("CheckAutoRebuildOnDestroyed")]


    public static class VFEProps_ThingUtility_CheckAutoRebuildOnDestroyed_Patch
    {


        [HarmonyPrefix]
        public static bool NoAutoRebuildProps(Thing thing)

        {
            if (thing != null && StaticCollections.props.Contains(thing.def))
            {
                return false;
            }
            return true;



        }

    }


}