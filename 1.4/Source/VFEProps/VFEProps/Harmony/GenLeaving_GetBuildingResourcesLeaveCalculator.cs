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


    [HarmonyPatch(typeof(GenLeaving))]
    [HarmonyPatch("DoLeavingsFor", new Type[] { typeof(Thing), typeof(Map), typeof(DestroyMode), typeof(CellRect), typeof(Predicate<IntVec3>), typeof(List<Thing>) })]
    public static class VFEProps_GenLeaving_DoLeavingsFor_Patch
    {

        [HarmonyPostfix]
        static void ReturnSilver(Thing diedThing, Map map)
        {

            if (diedThing!=null&&StaticCollections.props.Contains(diedThing.def) && diedThing.def.costList.NullOrEmpty() && map != null)
            {
                int silverAmount = 0;
                PropDef prop = (from x in DefDatabase<PropDef>.AllDefsListForReading
                                where x.prop == diedThing.def
                                select x).ToList().FirstOrDefault();

                if (prop.silverCostOverride != -1)
                {
                    silverAmount = prop.silverCostOverride;
                }
                else
                {
                    silverAmount = Utils.CostCalculator(prop.prop);
                }

                silverAmount = (int)(silverAmount* VFEProps_Settings.costMultiplier*VFEProps_Settings.silverReturnMultiplier);

                if (silverAmount != 0) {
                    Thing silver = ThingMaker.MakeThing(ThingDefOf.Silver);
                    GenPlace.TryPlaceThing(silver, diedThing.Position, map, ThingPlaceMode.Direct);
                    silver.stackCount = silverAmount;
                }
                
            }

        }
    }








}
