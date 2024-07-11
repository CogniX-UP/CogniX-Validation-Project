using Cognix.Validation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public abstract class PuzzleDirectionInput : MonoBehaviour
    {
        public abstract Puzzle.Direction Direction { get; }
    }
}

