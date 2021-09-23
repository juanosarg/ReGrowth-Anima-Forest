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

	}
}
