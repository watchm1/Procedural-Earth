using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.EarthBase
{
    
    public class ShapeGenerator
    {
        private ShapeSettings settings;
        private NoiseFilter noiseFilter;
        public ShapeGenerator(ShapeSettings settings)
        {
            this.settings = settings;
            noiseFilter = new NoiseFilter(settings.noiseSettings);
        }
        public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitySphere)
        {
            float elevation = noiseFilter.Evaluate(pointOnUnitySphere);
            return pointOnUnitySphere * settings.planetRadius * (1 + elevation);
        }
    }

}