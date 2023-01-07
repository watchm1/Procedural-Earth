using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Global.EarthBase
{
    public static class NoiseFilterFactory
    {
        public static INoiseFilter GenerateFilter(NoiseSettings settings)
        {
           switch (settings.type) {
            case NoiseSettings.FilterType.Simple:
                return new NoiseFilter(settings.simpleNoiseSettings);
            case NoiseSettings.FilterType.Rigid:
                return new RigidNoiseFilter(settings.rigidNoiseSettings);
           }
           return null;
        }
    }
}