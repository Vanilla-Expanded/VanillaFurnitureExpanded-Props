
using RimWorld;
using Verse;
using Verse.Sound;

using UnityEngine;
using static HarmonyLib.Code;
using System;

namespace VFEProps
{
    public class Building_NotifyColorChanged : Building_SubstractsSilver
    {
      
        public override void Notify_ColorChanged()
        {
            base.Notify_ColorChanged();
            CompMoteReleaser comp = this.TryGetComp<CompMoteReleaser>();
            if(comp != null)
            {
                comp.Notify_ColorChanged();
            }
        }



    }
}