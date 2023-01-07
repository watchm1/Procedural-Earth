using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.EarthBase
{
    public class NoiseFilter
    {
        NoiseSettings noiseSettings;
        private Noise noise = new Noise();
        public NoiseFilter(NoiseSettings settings)
        {
            this.noiseSettings = settings;
        }
        public float Evaluate(Vector3 point)
        {
            float noiseValue = (noise.Evaluate(point * noiseSettings.roughness + noiseSettings.center) + 1) * .5f;
            return noiseValue * noiseSettings.strength;
        }
    }

}