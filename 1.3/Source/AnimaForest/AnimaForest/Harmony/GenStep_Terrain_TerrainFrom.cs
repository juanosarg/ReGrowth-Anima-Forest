using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;
using System;
using RimWorld.BaseGen;


namespace AnimaForest
{

    /*This Harmony Postfix allows us to remove gravel from a certain biome
    */
    [HarmonyPatch(typeof(GenStep_Terrain))]
    [HarmonyPatch("TerrainFrom")]
    public static class AnimaForest_GenStep_Terrain_TerrainFrom_Patch
    {
        [HarmonyPostfix]
        public static void RemoveGravel(Map map, ref TerrainDef __result)

        {
            
            if ((__result == TerrainDefOf.Gravel) && (map.Biome == AF_DefOf.RG_AF_AnimaForest))
            {
                //Log.Message("Detectado e intentando cambiar");
                __result = AF_DefOf.RG_AnimaSoilCracked;
            }

        }

    }


}
