using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaForest
{

	public class Hediff_PsychicFogEffect : HediffWithComps
	{
        public override bool ShouldRemove => !pawn.IsAffectedByPsychicFog();
    }
}
