using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace VFEProps
{



    public class VFEProps_Mod : Mod
    {


        public VFEProps_Mod(ModContentPack content) : base(content)
        {
            GetSettings<VFEProps_Settings>();
        }
        public override string SettingsCategory() => "VFE - Props";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            VFEProps_Settings.DoWindowContents(inRect);
        }
    }


}
