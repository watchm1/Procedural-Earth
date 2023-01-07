using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.EarthBase
{
    public interface INoiseFilter
    {
        float Evaluate(Vector3 point);
    }
}
