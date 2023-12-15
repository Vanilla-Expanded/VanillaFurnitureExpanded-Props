using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace VFEProps
{
    public class PropDef : Def
    { 
        public float priority;
        public PropCategoryDef category;
        public List<PropCategoryDef> categories;
        public BuildableDef prop;
        public int silverCostOverride=-1;
        public string shortLabel = "";
        public bool useMatsInsteadOfSilver = false;
        public bool dontPopUpStupidGraphicErrors = false;
        
    }
}