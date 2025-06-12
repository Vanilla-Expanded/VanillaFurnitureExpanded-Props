
using RimWorld;
using Verse;
using Verse.Sound;

namespace VFEProps
{
    public class Building_FakeSteamGeyser : Building_SubstractsSilver
    {
        private IntermittentSteamSprayer steamSprayer;

        private Sustainer spraySustainer;

        private int spraySustainerStartTick = -999;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            steamSprayer = new IntermittentSteamSprayer(this);
            steamSprayer.startSprayCallback = StartSpray;
            steamSprayer.endSprayCallback = EndSpray;
        }

        private void StartSpray()
        {
         
            spraySustainer = SoundDefOf.GeyserSpray.TrySpawnSustainer(new TargetInfo(base.Position, base.Map));
            spraySustainerStartTick = Find.TickManager.TicksGame;
        }

        private void EndSpray()
        {
            if (spraySustainer != null)
            {
                spraySustainer.End();
                spraySustainer = null;
            }
        }

        protected override void Tick()
        {
            
            steamSprayer.SteamSprayerTick();
           
            if (spraySustainer != null && Find.TickManager.TicksGame > spraySustainerStartTick + 1000)
            {
                Log.Message("Fake Geyser spray sustainer still playing after 1000 ticks. Force-ending.");
                spraySustainer.End();
                spraySustainer = null;
            }
        }
    }
}