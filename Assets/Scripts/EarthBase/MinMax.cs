using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.EarthBase
{
    public class MinMax
    {
        public float Min { get; set; }
        public float Max { get; set; }
        public MinMax()
        {
            this.Min = float.MaxValue;
            this.Max = float.MinValue;
        }
        public void AddValue(float v)
        {
            if(v> Max)
            {
                Max = v;
            }
            if(v< Min)
            {
                Min = v;
            }
        }
    }
    
}