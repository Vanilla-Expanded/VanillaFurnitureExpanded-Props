using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using System;
using Verse.Noise;

namespace VFEProps
{


    [HarmonyPatch(typeof(RoyalTitlePermitWorker_CallShuttle))]
    [HarmonyPatch("GetReportFromCell")]
    public static class VFEProps_RoyalTitlePermitWorker_CallShuttle_GetReportFromCell_Patch
    {

        [HarmonyPostfix]
        static void LandOnDecals(IntVec3 cell, Map map, ref string __result)
        {
            List<Thing> thingList = cell.GetThingList(map);

            bool onlyDecals = true;
            for (int i = 0; i < thingList.Count; i++)
            {
                Thing thing = thingList[i];
                if ((thing.def.category == ThingCategory.Building && !thing.def.building.isPowerConduit && thing.def.defName.Contains("Decal")))
                {
                    onlyDecals = true;

                }
                else { onlyDecals = false;
                    break;
                }
               
            }
            if (onlyDecals) {
                __result = null;
            }
           


        }
    }








}
