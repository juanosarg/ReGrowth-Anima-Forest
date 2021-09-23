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

    /*This Harmony Postfix allows us to create new rock types than only appear in certain biomes
    */
    [HarmonyPatch(typeof(World))]
    [HarmonyPatch("NaturalRockTypesIn")]
    public static class AnimaForest_World_NaturalRockTypesIn_Patch
    {

       
        [HarmonyPostfix]
        public static void MakeRocksAccordingToBiome(int tile, ref World __instance, ref IEnumerable<ThingDef> __result)

        {
            if ((__instance.grid.tiles[tile].biome.defName == "BiomesIslands_Atoll"))
            {
                return;
            }

            if ((__instance.grid.tiles[tile].biome.defName.Contains("AB_")))
            {
                return;
            }

            if (__instance.grid.tiles[tile].biome == AF_DefOf.RG_AF_AnimaForest)
            {
                List<ThingDef> replacedList = new List<ThingDef>();
                ThingDef item = AF_DefOf.RG_Jadeite;
                replacedList.Add(item);

                __result = replacedList;
            }
            else
            {
                Rand.PushState();
                Rand.Seed = tile;
                List<ThingDef> list = (from d in DefDatabase<ThingDef>.AllDefs
                                       where d.category == ThingCategory.Building && d.building.isNaturalRock && !d.building.isResourceRock &&
                                       !d.IsSmoothed && d.defName != "GU_RoseQuartz" && d.defName != "AB_Mudstone" && d.defName != "AB_SlimeStone" &&
                                       d.defName != "GU_AncientMetals" && d.defName != "AB_Cragstone" && d.defName != "AB_Obsidianstone" && d.defName != "BiomesIslands_CoralRock"
                                       && d != AF_DefOf.RG_Jadeite
                                       select d).ToList<ThingDef>();
                int num = Rand.RangeInclusive(2, 3);
                if (num > list.Count)
                {
                    num = list.Count;
                }
                List<ThingDef> list2 = new List<ThingDef>();
                for (int i = 0; i < num; i++)
                {
                    ThingDef item = list.RandomElement<ThingDef>();
                    list.Remove(item);
                    list2.Add(item);
                }
                Rand.PopState();
                __result = list2;


            }



        }
    }


}
