using System;
using System.Linq;
using RimWorld;
using Verse;

namespace AnimaForest
{
    public class IncidentWorker_ExposedOreDeposit : IncidentWorker_AnimaForestHarmony
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            var map = parms.target as Map;
            if (map.GetAnimaForestTracker().exposedOreDepositPlace.IsValid)
            {
                return false;
            }
            return base.CanFireNowSub(parms);
        }
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = parms.target as Map;
            var cell = map.GetAnimaForestTracker().StartExposedOreDepositIncident();
            SendStandardLetter(this.def.letterLabel, this.def.letterText, LetterDefOf.PositiveEvent, parms, new TargetInfo(cell, map));
            return true;
        }
    }
}
