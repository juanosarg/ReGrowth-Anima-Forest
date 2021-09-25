using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaForest
{
	public class GameCondition_PsychicStorm : GameCondition
	{
		public override WeatherDef ForcedWeather()
		{
			return AF_DefOf.RG_AF_PsychicStorm;
		}
		public override void GameConditionTick()
		{
			List<Map> affectedMaps = base.AffectedMaps;
			if (Find.TickManager.TicksGame % 60 == 0)
			{
				foreach (var map in affectedMaps)
                {
					foreach (var pawn in map.mapPawns.AllPawnsSpawned)
                    {
						DoFasterAgingOn(pawn);
					}
				}
			}
		}
		public static void DoFasterAgingOn(Pawn p)
		{
			if (!p.Position.Roofed(p.Map) && p.RaceProps.IsFlesh && p.Map.weatherManager.CurWeatherPerceived == AF_DefOf.RG_AF_PsychicStorm)
			{
				p.ageTracker.AgeBiologicalTicks += (5 * 3600000) / 41;
			}
		}
		public override bool AllowEnjoyableOutsideNow(Map map)
		{
			return false;
		}
	}
}
