
using RimWorld;
using UnityEngine;
using Verse;
namespace VFEProps
{
    public class CompProperties_ConstantFireOverlay : CompProperties_FireOverlay
    {
       

        public int upOffset = 0;

        public bool bigGraphic = false;

        public bool blueGraphic = false;

        public CompProperties_ConstantFireOverlay()
        {
            compClass = typeof(CompConstantFireOverlay);
        }

       
    }
}
