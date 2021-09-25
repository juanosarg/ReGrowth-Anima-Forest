using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AnimaForest
{

	public class Hediff_PsychicInsanity : HediffWithComps
	{
        public override void Tick()
        {
            base.Tick();
            if (Find.TickManager.TicksGame % GenDate.TicksPerHour == 0 && Rand.Chance(0.01f))
            {
                if (!pawn.InMentalState)
                {
                    pawn.mindState.mentalStateHandler.TryStartMentalState(AF_DefOf.WanderConfused);
                }
            }
        }
    }
}
