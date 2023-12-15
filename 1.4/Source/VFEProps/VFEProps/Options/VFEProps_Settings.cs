using System.Collections.Generic;
using Verse;
using System.Linq;
using UnityEngine;
using System;



namespace VFEProps
{
    public class VFEProps_Settings : ModSettings

    {

        public static float costMultiplier = baseCostMultiplier;
        public const float baseCostMultiplier = 1f;

        public static float silverReturnMultiplier = baseSilverReturnMultiplier;
        public const float baseSilverReturnMultiplier = 1f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref costMultiplier, "costMultiplier", baseCostMultiplier);
            Scribe_Values.Look(ref silverReturnMultiplier, "silverReturnMultiplier", baseSilverReturnMultiplier);
        }

        public static void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();


            ls.Begin(inRect);
            ls.Gap(10f);

            var costLabel = ls.LabelPlusButton("VFE_PropCostMultiplier".Translate() + ": " + costMultiplier, "VFE_PropCostMultiplierDesc".Translate());
            costMultiplier = (float)Math.Round(ls.Slider(costMultiplier, 0f, 5), 1);

            if (ls.Settings_Button("VFE_Reset".Translate(), new Rect(0f, costLabel.position.y + 35, 250f, 29f)))
            {
                costMultiplier = baseCostMultiplier;
            }
            ls.Gap(10f);
            var silverLabel = ls.LabelPlusButton("VFE_SilverReturnMultiplier".Translate() + ": " + (silverReturnMultiplier).ToStringPercent(), "VFE_SilverReturnMultiplierDesc".Translate());
            silverReturnMultiplier = (float)Math.Round(ls.Slider(silverReturnMultiplier, 0f, 1), 1);

            if (ls.Settings_Button("VFE_Reset".Translate(), new Rect(0f, silverLabel.position.y + 35, 250f, 29f)))
            {
                silverReturnMultiplier = baseSilverReturnMultiplier;
            }


            ls.End();
        }



    }










}
