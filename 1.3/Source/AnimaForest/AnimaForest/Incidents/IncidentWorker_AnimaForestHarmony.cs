using System;
using System.Linq;
using RimWorld;
using Verse;

namespace AnimaForest
{
    public class IncidentWorker_AnimaForestHarmony : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            var map = parms.target as Map;
            if (map.Biome != AF_DefOf.RG_AF_AnimaForest || !map.GetAnimaForestTracker().InHarmony)
            {
                return false;
            }
            return base.CanFireNowSub(parms);
        }
    }
}
