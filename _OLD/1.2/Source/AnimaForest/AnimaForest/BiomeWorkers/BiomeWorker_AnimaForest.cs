using System;
using RimWorld.Planet;
using RimWorld;
using UnityEngine;
using Verse;

namespace AnimaForest
{
    public class BiomeWorker_AnimaForest : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            
            if (tile.WaterCovered)
            {
                return -100f;
            }
            if ((tile.temperature < 17f) || (tile.temperature > 25f))
            {
                return 0f;
            }
            if (tile.rainfall < 600f)
            {
                return 0f;
            }
            Vector3 tileCenter = Find.WorldGrid.GetTileCenter(tileID);
            if (Find.World.GetComponent<WorldComponentExtender>() == null)
            {
                WorldComponent item = (WorldComponent)Activator.CreateInstance(typeof(WorldComponentExtender), new object[]
               {
                        Find.World
               });
                Find.World.components.Add(item);
            }

            float tileAnima = Find.World.GetComponent<WorldComponentExtender>().noiseAnima.GetValue(tileCenter);
            
            if (tileAnima > 0.5f)
            {
                return 101f;
            }
            else return 0f;
            
        }
    }
}
