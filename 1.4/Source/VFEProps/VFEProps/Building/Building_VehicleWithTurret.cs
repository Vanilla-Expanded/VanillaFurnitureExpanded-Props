
using RimWorld;
using Verse;
using Verse.Sound;

using UnityEngine;

using System;

namespace VFEProps
{
    public class Building_VehicleWithTurret : Building_SubstractsSilver
    {
        public DrawTurretExtension extension = null;

        public Graphic graphic = null;

        public DrawTurretExtension GetExtension
        {
            get
            {
                if (extension is null)
                {
                    extension = this.def.GetModExtension<DrawTurretExtension>();
                   
                }
                return extension;
            }
        }

        public Graphic GetGraphic
        {
            get
            {
                if (graphic is null)
                {
                    LongEventHandler.ExecuteWhenFinished(delegate { GetGraphicLong(); });
                    
                }
                return graphic;
            }
        }

        public void GetGraphicLong()
        {
            try
            {
                Shader shader = GetExtension.forceNoMask ? ShaderDatabase.DefaultShader : ShaderDatabase.CutoutComplex;
                graphic = (Graphic_Single)GraphicDatabase.Get<Graphic_Single>(GetExtension.turretToDraw, shader, GetExtension.drawSize, DrawColor);
            }
            catch (Exception) {  }
        }

        public override void Draw()
        {
            base.Draw();
            var vector = this.DrawPos + Altitudes.AltIncVect;
            vector.y += 5;

            switch (this.Rotation.AsInt)
            {
                case 0: //north
                    vector.x += GetExtension.offset.north.x;
                    vector.z += GetExtension.offset.north.y;
                    break;
                case 1: //east
                    vector.x += GetExtension.offset.east.x;
                    vector.z += GetExtension.offset.east.y;
                    break;
                case 2: //south
                    vector.x += GetExtension.offset.south.x;
                    vector.z += GetExtension.offset.south.y;
                    break;
                case 3: //west
                    vector.x += GetExtension.offset.west.x;
                    vector.z += GetExtension.offset.west.y;
                    break;
            }
       
            GetGraphic?.DrawFromDef(vector, this.Rotation, null);

        }

        public override void Notify_ColorChanged()
        {
            base.Notify_ColorChanged();
            graphic = null;
            Map.mapDrawer.MapMeshDirty(Position, MapMeshFlag.Things);
            Draw();
        }



    }
}