using System;
using System.Linq;
using RimWorld;
using Verse;

namespace AnimaForest
{
    public class IncidentWorker_AnimaSoothe : IncidentWorker_AnimaForestHarmony
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = parms.target as Map;
            map.AddGameCondition(AF_DefOf.RG_AF_AnimaSoothe, GenDate.TicksPerDay * 5);
            SendStandardLetter(this.def.letterLabel, this.def.letterText, LetterDefOf.PositiveEvent, parms, null);
            return true;
        }
    }
}
