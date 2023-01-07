using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.EarthBase;
[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius = 1;
    public NoiseSettings noiseSettings;
}
