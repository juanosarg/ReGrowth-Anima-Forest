using System;
using System.Linq;
using RimWorld;
using Verse;

namespace AnimaForest
{
    public class IncidentWorker_PsychicShriek : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = parms.target as Map;
            var pawns = map.mapPawns.AllPawns.Where(x => !x.Dead && x.Spawned && x.RaceProps.Humanlike).ToList();
            foreach (var pawn in pawns)
            {
                pawn.mindState.mentalStateHandler.TryStartMentalState(AF_DefOf.RG_AF_Wander_Psychotic_Short);
            }
            SendStandardLetter(this.def.letterLabel, this.def.letterText, LetterDefOf.NegativeEvent, parms, pawns);
            return true;
        }
    }
}
