using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using UnityEngine;

namespace AnimaForest
{
    public class AnimaForestTracker : MapComponent
    {
        private int lastHarmTick;

        public const int MinHarmonyDuration = GenDate.TicksPerDay * 7;
        public bool InHarmony => (Find.TickManager.TicksGame - lastHarmTick) >= MinHarmonyDuration;
        public AnimaForestTracker(Map map) : base(map)
        {

        }
        public void RegisterHarm()
        {
            lastHarmTick = Find.TickManager.TicksGame;
            if (Rand.Chance(0.1f))
            {
                Utils.DoAnimaWrath(map);
            }
        }

        public int oreDepositIncidentEndTick;
        public IntVec3 exposedOreDepositPlace = IntVec3.Invalid;
        public IntVec3 StartExposedOreDepositIncident()
        {
            if (!exposedOreDepositPlace.IsValid && TryFindCell(out exposedOreDepositPlace))
            {
                oreDepositIncidentEndTick = Find.TickManager.TicksGame + 600;
                return exposedOreDepositPlace;
            }
            return IntVec3.Invalid;
        }
        public override void MapComponentTick()
        {
            base.MapComponentTick();
            if (exposedOreDepositPlace.IsValid)
            {
                if (oreDepositIncidentEndTick > Find.TickManager.TicksGame)
                {
                    Find.CameraDriver.shaker.DoShake(1f);
                    foreach (var cell in GenRadial.RadialCellsAround(exposedOreDepositPlace, ThingSetMaker_ExposedOreDeposit.MineablesCountRange.max / 4, true))
                    {
                        if (Rand.Chance(0.5f))
                        {
                            FleckMaker.ThrowDustPuffThick(cell.ToVector3Shifted() + new Vector3(Rand.Range(-0.02f, 0.02f), 0f, Rand.Range(-0.02f, 0.02f)), map, 1f, Color.green);
                        }
                    }
                }
                else
                {
                    List<Thing> list = AF_DefOf.RG_AF_ExposedOreDeposite.root.Generate();
                    foreach (var cell in GenRadial.RadialCellsAround(exposedOreDepositPlace, ThingSetMaker_ExposedOreDeposit.MineablesCountRange.max / 4, true))
                    {
                        if (list.Any())
                        {
                            GenSpawn.Spawn(list[0], cell, map);
                            list.RemoveAt(0);
                        }
                    }

                    oreDepositIncidentEndTick = -1;
                    exposedOreDepositPlace = IntVec3.Invalid;
                }
            }
        }
        private bool TryFindCell(out IntVec3 cell)
        {
            int maxMineables = ThingSetMaker_ExposedOreDeposit.MineablesCountRange.max;
            return CellFinderLoose.TryFindSkyfallerCell(ThingDefOf.MeteoriteIncoming, map, out cell, 10, default(IntVec3), -1, allowRoofedCells: true, allowCellsWithItems: false, allowCellsWithBuildings: false, colonyReachable: false, avoidColonistsIfExplosive: true, alwaysAvoidColonists: true, delegate (IntVec3 x)
            {
                int num = Mathf.CeilToInt(Mathf.Sqrt(maxMineables)) + 2;
                CellRect cellRect = CellRect.CenteredOn(x, num, num);
                int num2 = 0;
                foreach (IntVec3 item in cellRect)
                {
                    if (item.InBounds(map) && item.Standable(map))
                    {
                        num2++;
                    }
                }
                return num2 >= maxMineables;
            });
        }
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref lastHarmTick, "lastHarmTick");
            Scribe_Values.Look(ref oreDepositIncidentEndTick, "oreDepositIncidentEndTick");
            Scribe_Values.Look(ref exposedOreDepositPlace, "exposedOreDepositPlace", IntVec3.Invalid);
        }
    }
}
