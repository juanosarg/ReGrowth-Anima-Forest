using System;
using RimWorld;
using Verse;

namespace AnimaForest
{
	[DefOf]
	public static class AF_DefOf
	{
		static AF_DefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(AF_DefOf));
		}

		public static BiomeDef RG_AF_AnimaForest;
		public static TerrainDef RG_AnimaSoilCracked;
		public static ThingDef RG_Jadeite;
		public static WeatherDef RG_AF_PsychicFog;
		public static HediffDef RG_AF_PsychicFogEffect;
		public static WeatherDef RG_AF_PsychicStorm;
		public static MentalStateDef RG_AF_Wander_Psychotic_Short;

		public static GameConditionDef RG_AF_PsychicStormGC;
		public static GameConditionDef RG_AF_PsychicFlareGC;
		public static IncidentDef RG_AF_PsychicShriek;
		public static IncidentDef RG_AF_AnimalInsanityPulse;
		public static ThingDef RG_AnimaBear;
		public static ThingDef RG_AnimaDeer;
		public static ThingSetMakerDef RG_AF_ExposedOreDeposite;
		public static GameConditionDef RG_AF_AnimaSoothe;
		public static HediffDef RG_AF_PsychicBrainworm;
		public static HediffDef RG_AF_PsychicBrainwormParalysis;
		public static MentalStateDef WanderConfused;
	}
}
