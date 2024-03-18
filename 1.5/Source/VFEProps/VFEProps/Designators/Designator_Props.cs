using System;
using System.Collections.Generic;
using System.Linq;

using RimWorld;
using UnityEngine;
using Verse;

namespace VFEProps
{
    internal class Designator_Props : Designator_Cells
    {
        

        public override int DraggableDimensions => 2;

        public Designator_Props()
        {
            defaultLabel = "VFEPD_BuildProps".Translate();
            defaultDesc = "VFEPD_BuildPropsDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("UI/Gizmo_BuildProps", true);
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            useMouseIcon = true;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!c.InBounds(Map))
            {
                return false;
            }
            return true;
        }

        public override void ProcessInput(Event ev)
        {
            if (!CheckCanInteract())
            {
                return;
            }
            Window_PropsCategories categoriesWindow = new Window_PropsCategories();
            Find.WindowStack.Add(categoriesWindow);

        }

       
    }
}