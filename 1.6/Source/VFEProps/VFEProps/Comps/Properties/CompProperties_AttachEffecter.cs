
using RimWorld;
using UnityEngine;
using Verse;
namespace VFEProps
{
    public class CompProperties_AttachEffecter : CompProperties
    {
       

        public EffecterDef effecterDef;

     
        public CompProperties_AttachEffecter()
        {
            compClass = typeof(CompAttachEffecter);
        }

       
    }
}
