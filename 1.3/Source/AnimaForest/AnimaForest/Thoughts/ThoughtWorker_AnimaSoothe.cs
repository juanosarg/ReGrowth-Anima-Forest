using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaForest
{

	public class ThoughtWorker_AnimaSoothe : ThoughtWorker
	{
		protected override ThoughtState CurrentStateInternal(Pawn p)
		{
			if (p.IsAffectedByAnimaSoothe())
            {
				return ThoughtState.ActiveDefault;
			}
			return false;
		}
	}
}
