using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;
using System;
using RimWorld.BaseGen;
using Verse.AI;

namespace AnimaForest
{

    [HarmonyPatch(typeof(Fire))]
    [HarmonyPatch("SpawnSetup")]
    public static class Fire_SpawnSetup_Patch
    {
        public static void Postfix(Fire __instance, Map map, bool respawningAfterLoad)
        {
            if (!respawningAfterLoad && __instance.Map?.Biome == AF_DefOf.RG_AF_AnimaForest)
            {
                if (__instance.Map.weatherManager.RainRate < 0.01f)
                {
                    var weatherDef = WeatherDef.Named("Rain");
                    __instance.Map.weatherManager.TransitionTo(weatherDef);
                    Traverse.Create(__instance.Map.weatherDecider).Field("curWeatherDuration").SetValue(weatherDef.durationRange.RandomInRange);
                }
            }
        }
    }

    [HarmonyPatch]
    public static class GenSteps_Patch
    {
        private static List<string> genStepsToSkipOnAnimaForest = new List<string>
        {
            "ScatterRoadDebris",
            "ScatterCaveDebris",
            "AncientUtilityBuilding",
            "MechanoidRemains",
            "AncientTurret",
            "AncientMechs",
            "AncientLandingPad",
            "AncientFences",
            "AncientPipelineSection",
            "AncientJunkClusters",
            "AnimaTrees" // is being handled by custom scatter code, so we exclude it
        };
        public static IEnumerable<MethodBase> TargetMethods()
        {
            foreach (var genStepDef in DefDatabase<GenStepDef>.AllDefs)
            {
                if (genStepsToSkipOnAnimaForest.Contains(genStepDef.defName))
                {
                    yield return AccessTools.Method(genStepDef.genStep.GetType(), "Generate");
                }
            }
        }
        public static bool Prefix(Map map, GenStepParams parms)
        {
            if (map.Biome == AF_DefOf.RG_AF_AnimaForest)
            {
                return false;
            }
            return true;
        }
    }
}
