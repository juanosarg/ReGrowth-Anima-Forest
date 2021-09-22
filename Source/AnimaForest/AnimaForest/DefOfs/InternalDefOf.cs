using System;
using RimWorld;
using Verse;

namespace AnimaForest
{
	[DefOf]
	public static class InternalDefOf
	{
		static InternalDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
		}

		public static BiomeDef RG_AF_AnimaForest;

		public static TerrainDef RG_AnimaSoilCracked;

		public static ThingDef RG_Jadeite;

	}
}
