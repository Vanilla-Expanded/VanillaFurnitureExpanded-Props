using Verse;
using System.Collections.Generic;
using UnityEngine;

namespace VFEProps
{

    public class DrawTurretExtension : DefModExtension
    {

        public string turretToDraw;
        public Vector2 drawSize;
        public TurretRotationDef offset;
        public bool forceNoMask = false;
    }

}
