
using RimWorld;
using UnityEngine;
using Verse;
namespace VFEProps
{
    [StaticConstructorOnStartup]
    public class CompConstantFireOverlay : CompFireOverlayBase
    {
        public const int FireGlowIntervalTicks = 30;

        public static readonly Graphic FireGraphic = GraphicDatabase.Get<Graphic_Flicker>("Things/Special/Fire", ShaderDatabase.TransparentPostLight, Vector2.one, Color.white);
        public static readonly Graphic FireGraphic2 = GraphicDatabase.Get<Graphic_Flicker>("Things/Building/ModCompat/LargeFire", ShaderDatabase.TransparentPostLight, Vector2.one, Color.white);
        public static readonly Graphic FireGraphic3 = GraphicDatabase.Get<Graphic_Flicker>("Things/Special/Darklight", ShaderDatabase.TransparentPostLight, Vector2.one, Color.white);


        public new CompProperties_ConstantFireOverlay Props => (CompProperties_ConstantFireOverlay)props;

        public override void PostDraw()
        {
            base.PostDraw();

            Vector3 loc = this.parent.TrueCenter() + (Vector3.forward * Props.upOffset);
            loc.y = AltitudeLayer.MoteOverhead.AltitudeFor();
            if (Props.bigGraphic)
            {
                FireGraphic2.Draw(loc, Rot4.North, this.parent, 0f);
            }
            else if (Props.blueGraphic)
            {
                FireGraphic3.Draw(loc, Rot4.North, this.parent, 0f);
            }
            else
            {
                FireGraphic.Draw(loc, Rot4.North, this.parent, 0f);
            }
           

        }
        public override void CompTick()
        {

            if (this.startedGrowingAtTick < 0)
            {
                this.startedGrowingAtTick = GenTicks.TicksAbs;
            }
            if (GenTicks.TicksAbs % FireGlowIntervalTicks == 0 && !Props.blueGraphic)
            {
                FleckMaker.ThrowFireGlow(this.parent.TrueCenter(), this.parent.Map, 1f);
            }
        }



    }
}
