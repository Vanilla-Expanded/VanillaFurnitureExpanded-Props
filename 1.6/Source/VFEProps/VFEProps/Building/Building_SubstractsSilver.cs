
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Verse;
using Verse.Sound;

namespace VFEProps
{
    public class Building_SubstractsSilver : Building
    {

        public bool swappingMap = false;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (!respawningAfterLoad && !swappingMap)
            {
                int cost = GetSilverCost();
                if (cost != 0)
                {
                    if (CheckSilverInMap(cost))
                    {
                        RemoveSilverFromMap(cost);
                    }
                    else
                    {
                        Messages.Message("VFE_NoSilver".Translate(cost), null, MessageTypeDefOf.RejectInput);
                        this.DeSpawn();
                    }
                }

            }
        }


        public int GetSilverCost()
        {
            PropDef prop = (from x in DefDatabase<PropDef>.AllDefsListForReading
                            where x.prop == this.def
                            select x).First();
            int cost = 0;
            if (!prop.useMatsInsteadOfSilver)
            {
                if (prop.silverCostOverride != -1)
                {
                    cost = prop.silverCostOverride;
                }
                else
                {

                    cost = Utils.CostCalculator(this.def);

                }
            }

            return (int)(cost * VFEProps_Settings.costMultiplier);
        }

        public bool CheckSilverInMap(int cost)
        {
            int totalSilver = 0;
            List<SlotGroup> allGroupsListForReading = Find.CurrentMap.haulDestinationManager.AllGroupsListForReading;
            for (int i = 0; i < allGroupsListForReading.Count; i++)
            {
                foreach (Thing heldThing in allGroupsListForReading[i].HeldThings)
                {
                    Thing innerIfMinified = heldThing.GetInnerIfMinified();
                    if (innerIfMinified.def.CountAsResource)
                    {
                        if (innerIfMinified.def == ThingDefOf.Silver)
                        {
                            totalSilver += innerIfMinified.stackCount;
                        }

                    }
                }
            }
            if (totalSilver >= cost)
            {
                return true;
            }

            return false;
        }

        public void RemoveSilverFromMap(int cost)
        {
            int silverLeftToRemove = cost;
            List<SlotGroup> allGroupsListForReading = Find.CurrentMap.haulDestinationManager.AllGroupsListForReading;

            for (int i = 0; i < allGroupsListForReading.Count; i++)
            {
                if (silverLeftToRemove <= 0) { break; }
                foreach (Thing heldThing in allGroupsListForReading[i].HeldThings)
                {
                    Thing innerIfMinified = heldThing.GetInnerIfMinified();
                    if (innerIfMinified.def.CountAsResource)
                    {
                        if (innerIfMinified.def == ThingDefOf.Silver)
                        {
                            int num = Math.Min(silverLeftToRemove, innerIfMinified.stackCount);
                            innerIfMinified.SplitOff(num).Destroy();

                            silverLeftToRemove -= num;

                            if (silverLeftToRemove <= 0) { break; }
                        }
                    }
                }
            }
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }

            Command_Action designator = new Command_Action();
            designator.action = delegate
            {
                Designator_Build designatorOnMouse = new Designator_Build(this.def);

                Find.DesignatorManager.Select(designatorOnMouse);
            };
            designator.defaultLabel = "VFEPD_MakeCopy".Translate();
            designator.defaultDesc = "VFEPD_MakeCopyDesc".Translate();
            designator.icon = this.def.uiIcon;
            yield return designator;
            
        }

        public override void PreSwapMap()
        {
            base.PreSwapMap();
            swappingMap = true;
        }

        public override void PostSwapMap()
        {
            base.PostSwapMap();
            swappingMap = false;
        }
    }


}
