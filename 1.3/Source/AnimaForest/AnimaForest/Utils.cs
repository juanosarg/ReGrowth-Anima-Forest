using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaForest
{
	[StaticConstructorOnStartup]
	public static class Utils
	{
		static Utils()
        {
			ThingSetMaker_ExposedOreDeposit.Reset();
		}
		public static bool IsAffectedByPsychicFog(this Pawn pawn)
        {
			return pawn.RaceProps.IsFlesh && pawn.Map != null && (pawn.GetRoom()?.PsychologicallyOutdoors ?? false) && pawn.Map.weatherManager.CurWeatherPerceived == AF_DefOf.RG_AF_PsychicFog;
		}

		public static bool IsAffectedByAnimaSoothe(this Pawn pawn)
		{
			return pawn.RaceProps.IsFlesh && pawn.Map != null && pawn.Map.gameConditionManager.ConditionIsActive(AF_DefOf.RG_AF_AnimaSoothe);
		}

		public static void DoAnimaWrath(Map map)
        {
			switch (Rand.RangeInclusive(1, 4))
			{
				case 1: AddGameCondition(map, AF_DefOf.RG_AF_PsychicStormGC, Rand.Range(GenDate.TicksPerHour * 2, GenDate.TicksPerHour * 3)); break;
				case 2: AddGameCondition(map, AF_DefOf.RG_AF_PsychicFlareGC, Rand.Range(GenDate.TicksPerDay, GenDate.TicksPerDay * 3)); break;
				case 3: AddIncident(map, AF_DefOf.RG_AF_PsychicShriek); break;
				case 4: AddIncident(map, AF_DefOf.RG_AF_AnimalInsanityPulse); break;
			}
		}
		public static void AddGameCondition(this Map map, GameConditionDef gameConditionDef, int duration)
        {
			var gameCondition = GameConditionMaker.MakeCondition(gameConditionDef, duration);
			map.gameConditionManager.RegisterCondition(gameCondition);
        }

		private static void AddIncident(Map map, IncidentDef incidentDef)
        {
			var parms = StorytellerUtility.DefaultParmsNow(incidentDef.category, map);
			incidentDef.Worker.TryExecute(parms);
		}

		private static Dictionary<Map, AnimaForestTracker> cachedComps = new Dictionary<Map, AnimaForestTracker>();
		public static AnimaForestTracker GetAnimaForestTracker(this Map map)
		{
			if (!cachedComps.TryGetValue(map, out var tracker))
			{
				cachedComps[map] = tracker = map.GetComponent<AnimaForestTracker>();
			}
			return tracker;
		}
	}
}
