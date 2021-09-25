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

    [HarmonyPatch(typeof(JobDriver_PlantCut))]
    [HarmonyPatch("PlantWorkDoneToil")]
    public static class JobDriver_PlantCut_PlantWorkDoneToil_Patch
    {
        public static void Postfix(Toil __result)
        {
            __result.AddFinishAction(() =>
            {
                if (__result.actor.Map.Biome == AF_DefOf.RG_AF_AnimaForest && Rand.Chance(0.1f))
                {
                    __result.actor.Map.GetAnimaForestTracker().RegisterHarm();
                }
            });
        }
    }

    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("Kill")]
    public static class Pawn_Kill_Patch
    {
        public static void Postfix(Pawn __instance, DamageInfo? dinfo, Hediff exactCulprit = null)
        {
            if (__instance.Map?.Biome == AF_DefOf.RG_AF_AnimaForest && __instance.Faction is null && dinfo.HasValue 
                && (dinfo.Value.Instigator is Pawn pawn && pawn.IsColonist || dinfo.Value.Instigator?.Faction == Faction.OfPlayer))
            {
                __instance.Map.GetAnimaForestTracker().RegisterHarm();
            }
        }
    }
}
