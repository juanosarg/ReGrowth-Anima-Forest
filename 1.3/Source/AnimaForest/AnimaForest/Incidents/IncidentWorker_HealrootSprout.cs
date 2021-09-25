using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace AnimaForest
{
    public class IncidentWorker_HealrootSprout : IncidentWorker_AnimaForestHarmony
    {
		private IntRange CountRange
        {
            get
            {
				if (PlantDef.defName == "VFEM_Plant_TreeHealroot")
                {
					return new IntRange(1, 1);
				}
				return new IntRange(10, 20);
			}
        }

		private const int MinRoomCells = 64;

		private const int SpawnRadius = 6;

		public ThingDef PlantDef
        {
            get
            {
				var def = DefDatabase<ThingDef>.GetNamedSilentFail("VFEM_Plant_TreeHealroot");
				if (def is null)
                {
					def = ThingDef.Named("Plant_Healroot");
                }
				return def;
            }
        }
		protected override bool CanFireNowSub(IncidentParms parms)
		{
			if (!base.CanFireNowSub(parms))
			{
				return false;
			}
			Map map = (Map)parms.target;
			if (!map.weatherManager.growthSeasonMemory.GrowthSeasonOutdoorsNow)
			{
				return false;
			}
			IntVec3 cell;
			return TryFindRootCell(map, out cell);
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			if (!TryFindRootCell(map, out var cell))
			{
				return false;
			}
			Thing thing = null;
			int randomInRange = CountRange.RandomInRange;
			for (int i = 0; i < randomInRange; i++)
			{
				if (!CellFinder.TryRandomClosewalkCellNear(cell, map, 6, out var result, (IntVec3 x) => CanSpawnAt(x, map)))
				{
					break;
				}
				result.GetPlant(map)?.Destroy();
				Thing thing2 = GenSpawn.Spawn(PlantDef, result, map);
				if (thing == null)
				{
					thing = thing2;
				}
			}
			if (thing == null)
			{
				return false;
			}
			SendStandardLetter(this.def.letterLabel, this.def.letterText, LetterDefOf.PositiveEvent, parms, thing, thing.Named("PLANT"));
			return true;
		}


		protected new void SendStandardLetter(TaggedString baseLetterLabel, TaggedString baseLetterText, LetterDef baseLetterDef, IncidentParms parms, LookTargets lookTargets, params NamedArgument[] textArgs)
		{
			if (!parms.sendLetter)
			{
				return;
			}
			if (baseLetterLabel.NullOrEmpty() || baseLetterText.NullOrEmpty())
			{
				Log.Error("Sending standard incident letter with no label or text.");
			}
			TaggedString taggedString = baseLetterText.Formatted(textArgs);
			TaggedString text;
			if (parms.customLetterText.NullOrEmpty())
			{
				text = taggedString;
			}
			else
			{
				List<NamedArgument> list = new List<NamedArgument>();
				if (textArgs != null)
				{
					list.AddRange(textArgs);
				}
				list.Add(taggedString.Named("BASETEXT"));
				text = parms.customLetterText.Formatted(list.ToArray());
			}
			TaggedString taggedString2 = baseLetterLabel.Formatted(textArgs);
			TaggedString label;
			if (parms.customLetterLabel.NullOrEmpty())
			{
				label = taggedString2;
			}
			else
			{
				List<NamedArgument> list2 = new List<NamedArgument>();
				if (textArgs != null)
				{
					list2.AddRange(textArgs);
				}
				list2.Add(taggedString2.Named("BASELABEL"));
				label = parms.customLetterLabel.Formatted(list2.ToArray());
			}
			ChoiceLetter choiceLetter = LetterMaker.MakeLetter(label.CapitalizeFirst(), text, parms.customLetterDef ?? baseLetterDef, lookTargets, parms.faction, parms.quest, parms.letterHyperlinkThingDefs);
			List<HediffDef> list3 = new List<HediffDef>();
			if (!parms.letterHyperlinkHediffDefs.NullOrEmpty())
			{
				list3.AddRange(parms.letterHyperlinkHediffDefs);
			}
			if (!def.letterHyperlinkHediffDefs.NullOrEmpty())
			{
				if (list3 == null)
				{
					list3 = new List<HediffDef>();
				}
				list3.AddRange(def.letterHyperlinkHediffDefs);
			}
			choiceLetter.hyperlinkHediffDefs = list3;
			Find.LetterStack.ReceiveLetter(choiceLetter);
		}

		private bool TryFindRootCell(Map map, out IntVec3 cell)
		{
			return CellFinderLoose.TryFindRandomNotEdgeCellWith(10, (IntVec3 x) => CanSpawnAt(x, map) && x.GetRoom(map).CellCount >= 64, map, out cell);
		}

		private bool CanSpawnAt(IntVec3 c, Map map)
		{
			if (!c.Standable(map) || c.Fogged(map) || map.fertilityGrid.FertilityAt(c) < PlantDef.plant.fertilityMin || !c.GetRoom(map).PsychologicallyOutdoors || c.GetEdifice(map) != null || !PlantUtility.GrowthSeasonNow(c, map))
			{
				return false;
			}
			Plant plant = c.GetPlant(map);
			if (plant != null && plant.def.plant.growDays > 10f)
			{
				return false;
			}
			List<Thing> thingList = c.GetThingList(map);
			for (int i = 0; i < thingList.Count; i++)
			{
				if (thingList[i].def == PlantDef)
				{
					return false;
				}
			}
			return true;
		}
    }
}
