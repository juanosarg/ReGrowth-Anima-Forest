using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaForest
{

	public static class Utils
	{
		public static bool IsAffectedByPsychicFog(this Pawn pawn)
        {
			return pawn.RaceProps.IsFlesh && pawn.Map != null && (pawn.GetRoom()?.PsychologicallyOutdoors ?? false) && pawn.Map.weatherManager.CurWeatherPerceived == AF_DefOf.RG_AF_PsychicFog;
		}
	}
}
