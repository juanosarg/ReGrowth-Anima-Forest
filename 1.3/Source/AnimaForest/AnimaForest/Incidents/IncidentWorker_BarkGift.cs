using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace AnimaForest
{
    public class IncidentWorker_BarkGift : IncidentWorker_AnimaForestHarmony
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = parms.target as Map;
            var trees = map.listerThings.AllThings.Where(x => x is Plant && x.def.plant.IsTree).ToList();
            var woods = new List<Thing>();
            foreach (var tree in trees)
            {
                var wood = ThingMaker.MakeThing(ThingDefOf.WoodLog);
                wood.stackCount = Rand.RangeInclusive(5, 15);
                GenPlace.TryPlaceThing(wood, tree.Position, tree.Map, ThingPlaceMode.Direct);
                woods.Add(wood);
            }
            SendStandardLetter(this.def.letterLabel, this.def.letterText, LetterDefOf.PositiveEvent, parms, woods);
            return true;
        }
    }
}
