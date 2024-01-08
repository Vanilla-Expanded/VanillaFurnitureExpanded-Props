
using RimWorld;
using Verse;
using Verse.Sound;

using UnityEngine;
using static HarmonyLib.Code;
using System;

namespace VFEProps
{
    public class Building_FakePlant5x5 : Building_SubstractsSilver
    {
        private static int[][][] rootList;


        public static int[] GetPositionIndices(Building_FakePlant5x5 p)
        {
            FakePosIndices();
            int maxMeshCount = 25;
            int num = (p.thingIDNumber ^ 0x2862FF0) % 8;
            return rootList[maxMeshCount - 1][num];
        }

        static void FakePosIndices()
        {
            rootList = new int[25][][];
            for (int i = 0; i < 25; i++)
            {
                rootList[i] = new int[8][];
                for (int j = 0; j < 8; j++)
                {
                    int[] array = new int[i + 1];
                    for (int k = 0; k < i; k++)
                    {
                        array[k] = k;
                    }
                    array.Shuffle();
                    rootList[i][j] = array;
                }
            }
        }


        public override void Print(SectionLayer layer)
        {
            Vector3 a = this.TrueCenter();
            Rand.PushState();
            Rand.Seed = base.Position.GetHashCode();
            int num = 25;
            float num2 = def.graphic.drawSize.x;
            Vector3 center = Vector3.zero;
            int num4 = 0;
            int[] positionIndices = GetPositionIndices(this);
           
            foreach (int num5 in positionIndices)
            {

                int num7 = 5;

                float num8 = 1f / (float)num7;
                center = base.Position.ToVector3();
                center.y = def.Altitude;
                center.x += 0.5f * num8;
                center.z += 0.5f * num8;
                int num9 = num5 / num7;
                int num10 = num5 % num7;
                center.x += (float)num9 * num8;
                center.z += (float)num10 * num8;
                float max = num8 * 0.3f;
                center += Gen.RandomHorizontalVector(max);

                bool @bool = Rand.Bool;
                Material material = Graphic.MatSingle;
                Graphic.TryGetTextureAtlasReplacementInfo(material, def.category.ToAtlasGroup(), @bool, vertexColors: false, out material, out Vector2[] uvs, out Color32 _);

                Printer_Plane.PrintPlane(size: new Vector2(num2, num2), layer: layer, center: center, mat: material, rot: 0f, flipUv: @bool, uvs: uvs, topVerticesAltitudeBias: 0.1f, uvzPayload: this.HashOffset() % 1024);
                num4++;
                if (num4 >= num)
                {
                    break;
                }
            }

            Rand.PopState();
        }


    }
}