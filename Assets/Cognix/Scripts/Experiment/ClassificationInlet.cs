using Cognix.LSL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public class ClassificationInlet : CognixFloatInlet
    {
        public float leftProbability = 0f;
        public float rightProbability = 0f;

        protected override void Process(float[] newSample, double timestamp)
        {
            leftProbability = SampleChannel("left", newSample);
            rightProbability = SampleChannel("right", newSample);
        }
    }
}

