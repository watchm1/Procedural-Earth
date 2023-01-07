using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.EarthBase;

namespace Global.EarthBase
{
    public class RigidNoiseFilter : INoiseFilter
    {
            NoiseSettings.RigidNoiseSettings noiseSettings;
            private Noise noise = new Noise();
            public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings)
            {
                this.noiseSettings = settings;
            }
            public float Evaluate(Vector3 point)
            {
                float noiseValue = 0;
                float frequency = noiseSettings.baseRoughness;
                float amplitude = 1;
                float weight = 1;
                for(int i = 0; i< noiseSettings.numLayers; i++)
                {
                    float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + noiseSettings.center));
                    v *= v;
                    v*= weight;
                    weight =Mathf.Clamp01( v * noiseSettings.wightMultiplier);

                    noiseValue += v * amplitude;
                    frequency *= noiseSettings.roughness;
                    amplitude *= noiseSettings.persistence;
                }
                noiseValue = Mathf.Max(0, noiseValue - noiseSettings.minValue);
                return noiseValue * noiseSettings.strength;
            }
    }

}