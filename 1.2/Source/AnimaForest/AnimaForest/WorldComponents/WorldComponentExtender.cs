using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.Noise;
using UnityEngine;
using RimWorld.Planet;

namespace AnimaForest
{
    public class WorldComponentExtender : WorldComponent
    {

        [Unsaved]
        public ModuleBase noiseAnima;      

        private static readonly FloatRange RadMaxElevation = new FloatRange(650f, 750f);


        private static float FreqMultiplier
        {
            get
            {
                return .5f;
            }
        }

        public WorldComponentExtender(World world) : base(world)
        {
            SetupAnimaNoise();         
        }


        private void SetupAnimaNoise()
        {
            float freqMultiplier = FreqMultiplier;
            ModuleBase moduleBase = new Perlin((double)(0.09f * freqMultiplier), 2.0, 0.40000000596046448, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
            ModuleBase moduleBase2 = new RidgedMultifractal((double)(0.025f * freqMultiplier), 2.0, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
            moduleBase = new ScaleBias(0.5, 0.5, moduleBase);
            moduleBase2 = new ScaleBias(0.5, 0.5, moduleBase2);
            this.noiseAnima = new Multiply(moduleBase, moduleBase2);
            InverseLerp rhs = new InverseLerp(this.noiseAnima, RadMaxElevation.max, RadMaxElevation.min);
            this.noiseAnima = new Multiply(this.noiseAnima, rhs);

            NoiseDebugUI.StorePlanetNoise(this.noiseAnima, "noiseAnima");
        }

   

    }
}
