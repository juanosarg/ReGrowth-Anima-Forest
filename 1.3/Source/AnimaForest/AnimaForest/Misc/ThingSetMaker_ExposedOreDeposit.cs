using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace AnimaForest
{
	public class ThingSetMaker_ExposedOreDeposit : ThingSetMaker
	{
		public static List<ThingDef> nonSmoothedMineables = new List<ThingDef>();

		public static readonly IntRange MineablesCountRange = new IntRange(8, 20);
		public static void Reset()
		{
			nonSmoothedMineables.Clear();
			nonSmoothedMineables.AddRange(DefDatabase<ThingDef>.AllDefsListForReading.Where((ThingDef x) => x.mineable && x != ThingDefOf.CollapsedRocks && x != ThingDefOf.RaisedRocks 
			&& !x.IsSmoothed && x.building.isResourceRock));
		}
		protected override void Generate(ThingSetMakerParams parms, List<Thing> outThings)
		{
			int randomInRange = (parms.countRange ?? MineablesCountRange).RandomInRange;
			ThingDef def = FindRandomMineableDef();
			for (int i = 0; i < randomInRange; i++)
			{
				Building building = (Building)ThingMaker.MakeThing(def);
				building.canChangeTerrainOnDestroyed = false;
				outThings.Add(building);
			}
		}
		private ThingDef FindRandomMineableDef()
		{
			return nonSmoothedMineables.RandomElement();
		}

		protected override IEnumerable<ThingDef> AllGeneratableThingsDebugSub(ThingSetMakerParams parms)
		{
			return nonSmoothedMineables;
		}
	}
}
