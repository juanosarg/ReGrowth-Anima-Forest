using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaForest
{

	public class ThoughtWorker_PsychicFog : ThoughtWorker
	{
		protected override ThoughtState CurrentStateInternal(Pawn p)
		{
			if (p.IsAffectedByPsychicFog())
            {
				var hediff = p.health.hediffSet.GetFirstHediffOfDef(AF_DefOf.RG_AF_PsychicFogEffect);
				if (hediff is null)
                {
					hediff = HediffMaker.MakeHediff(AF_DefOf.RG_AF_PsychicFogEffect, p);
					p.health.AddHediff(hediff);
                }
				return ThoughtState.ActiveDefault;
			}
			return false;
		}
	}
}
