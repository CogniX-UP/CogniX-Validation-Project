using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.LSL
{
    [Serializable]
    public struct GazePosition
    {
        public float confidence;
        public Vector2 pos;
        public override string ToString() =>
            $"confidence: {confidence} pos: {pos}";
    }
}