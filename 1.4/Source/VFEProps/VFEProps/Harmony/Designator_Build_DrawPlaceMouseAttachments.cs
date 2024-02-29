using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using System.Linq;
using UnityEngine;



namespace VFEProps
{


    [HarmonyPatch(typeof(Designator_Build))]
    [HarmonyPatch("DrawPlaceMouseAttachments")]


    public static class VFEProps_Designator_Build_DrawPlaceMouseAttachments_Patch
    {
      

        [HarmonyPostfix]
        public static void AddSilverCost(BuildableDef ___entDef, float curY, float curX)

        {
            if(___entDef!=null&&StaticCollections.props.Contains(___entDef)&& ___entDef.costList.NullOrEmpty())
            {

                float y = curY;
                Widgets.ThingIcon(new Rect(curX, y, 27f, 27f), ThingDefOf.Silver);
                Rect rect2 = new Rect(curX + 29f, y, 999f, 29f);
                int num2 = (int)(Utils.CostCalculator(___entDef) * VFEProps_Settings.costMultiplier);
                string text = num2.ToString();
               
                Text.Font = GameFont.Small;
                Text.Anchor = TextAnchor.MiddleLeft;
                Widgets.Label(rect2, text);
                Text.Anchor = TextAnchor.UpperLeft;
                GUI.color = Color.white;

            }



        }

    }


}