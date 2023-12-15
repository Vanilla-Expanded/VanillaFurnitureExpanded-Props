using System.Collections.Generic;
using Verse;
using System.Linq;
using UnityEngine;
using System;
using RimWorld;
using UnityEngine.UIElements;

namespace VFEProps
{
    public static class Utils
    {


      

        public static int CostCalculator(BuildableDef def) {

            if (Prefs.DevMode)
            {
                return 0;
            }

            ThingDef thingDef = def as ThingDef;
            if (def != null)
            {
                return Math.Max(5, (int)(7.5 * ((float)thingDef.BaseMaxHitPoints / 300) * (thingDef.fillPercent / 0.55f) * (def.Size.x * def.Size.z)));

            }
            else return 0;

        }

        public static void DoInfoBox(Rect infoRect, Designator_Build designator)
        {

          

            Find.WindowStack.ImmediateWindow(32519, infoRect, WindowLayer.GameUI, delegate
            {
                if (designator != null)
                {
                   Rect rect = infoRect.AtZero().ContractedBy(7f);
                     Widgets.BeginGroup(rect);
                     Rect rect2 = new Rect(0f, 0f, rect.width - designator.PanelReadoutTitleExtraRightMargin, 999f);
                     Text.Font = GameFont.Small;
                     Widgets.Label(rect2, designator.LabelCap);
                     float curY = Mathf.Max(24f, Text.CalcHeight(designator.LabelCap, rect2.width));
                     designator.DrawPanelReadout(ref curY, rect.width);
                     Rect rect3 = new Rect(0f, curY, rect.width, rect.height - curY);
                     string desc = designator.Desc;
                     GenText.SetTextSizeToFit(desc, rect3);
                     desc = desc.TruncateHeight(rect3.width, rect3.height);
                     Widgets.Label(rect3, desc);
                     Widgets.EndGroup();
                }
                
            });
        }


    }










}
