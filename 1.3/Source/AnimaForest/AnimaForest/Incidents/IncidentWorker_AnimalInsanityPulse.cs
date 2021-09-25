using System;
using System.Linq;
using RimWorld;
using Verse;

namespace AnimaForest
{
    public class IncidentWorker_AnimalInsanityPulse : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = parms.target as Map;
            var pawns = map.mapPawns.AllPawns.Where(x => !x.Dead && x.Spawned && x.RaceProps.Animal).ToList();
            foreach (var pawn in pawns)
            {
                pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Manhunter);
            }
            SendStandardLetter(this.def.letterLabel, this.def.letterText, LetterDefOf.NegativeEvent, parms, pawns);
            return true;
        }
    }
}
