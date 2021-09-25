using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaForest
{
	public class GameCondition_AnimaSoothe : GameCondition
	{
		public override bool AllowEnjoyableOutsideNow(Map map)
		{
			return true;
		}
	}
}
