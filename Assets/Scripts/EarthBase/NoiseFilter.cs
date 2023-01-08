using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.EarthBase
{
    public class NoiseFilter : INoiseFilter
    {
        NoiseSettings.SimpleNoiseSettings noiseSettings;
        private Noise noise = new Noise();
        public NoiseFilter(NoiseSettings.SimpleNoiseSettings settings)
        {
            this.noiseSettings = settings;
        }
        public float Evaluate(Vector3 point)
        {
            float noiseValue = 0;
            float frequency = noiseSettings.baseRoughness;
            float amplitude = 1;
            for(int i = 0; i< noiseSettings.numLayers; i++)
            {
                float v = noise.Evaluate(point * frequency + noiseSettings.center);
                noiseValue += (v+1) * .5f * amplitude;
                frequency *= noiseSettings.roughness;
                amplitude *= noiseSettings.persistence;
            }
            noiseValue = noiseValue - noiseSettings.minValue;
            return noiseValue * noiseSettings.strength;
        }
    }

}