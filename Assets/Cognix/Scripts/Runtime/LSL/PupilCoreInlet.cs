using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL4Unity.Utils;
using System;

namespace Cognix.LSL
{
    /// <summary>
    /// An inlet for capturing Eye Gaze Data in a user friendly way in Unity
    /// </summary>
    public class PupilCoreInlet : CognixFloatInlet
    {
        [Range(0, 1)]
        public float confidenceMargin = 0;
        public event Action<GazePosition> onGazePosition;
        protected override void Process(float[] newSample, double timestamp)
        {
            var conf = SampleChannel("confidence", newSample);
            if (conf < confidenceMargin)
                return;
            var gaze = new GazePosition
            {
                confidence = conf,
                pos = new Vector2
                {
                    x = SampleChannel("norm_pos_x", newSample),
                    y = SampleChannel("norm_pos_y", newSample),
                }
            };

            onGazePosition?.Invoke(gaze);
        }
    }
}

