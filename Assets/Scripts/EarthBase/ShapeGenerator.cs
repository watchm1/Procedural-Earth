using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.EarthBase
{
    
    public class ShapeGenerator
    {
        private ShapeSettings settings;
        private INoiseFilter[] noiseFilters;
        public MinMax elevationMinMax;
        public void UpdateSettings(ShapeSettings settings)
        {
            this.settings = settings;
            noiseFilters = new INoiseFilter[settings.noiseLayers.Length];
            for(int i = 0; i < noiseFilters.Length; i++)
            {
                noiseFilters[i] = NoiseFilterFactory.GenerateFilter(settings.noiseLayers[i].noiseSettings);
            }
            elevationMinMax = new MinMax();
        }
        public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitySphere)
        {
            float firstLayer = 0;
            float elevation = 0;
            if(noiseFilters.Length > 0)
            {
                firstLayer = noiseFilters[0].Evaluate(pointOnUnitySphere);
                if(settings.noiseLayers[0].enabled)
                {
                    elevation = firstLayer;
                }
            }
            for(int i = 0; i < noiseFilters.Length ; i++)
            {
                if(settings.noiseLayers[i].enabled)
                {
                    float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayer : 1;
                    elevation += noiseFilters[i].Evaluate(pointOnUnitySphere) * mask;
                }
            }
            elevation =  settings.planetRadius * (1 + elevation);
            elevationMinMax.AddValue(elevation);
            return pointOnUnitySphere * elevation;
        }
    }

}