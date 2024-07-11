using Cognix.LSL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public class EyeGazePointer : PuzzlePointer
    {
        [SerializeField] PupilCoreInlet eyeInlet;
        [SerializeField] Vector2 dir = Vector2.one;
        [SerializeField] Vector2 surfToUnityOffset = Vector2.zero;
        Queue<GazePosition> gazeQueue = new Queue<GazePosition>();

        [SerializeField] Vector2 lastPosition = Vector2.zero;
        
        private void OnEnable() => eyeInlet.onGazePosition += OnGaze;
        private void OnDisable() => eyeInlet.onGazePosition -= OnGaze;
        public override Vector2 Point
        {
            get
            {
                // Keep the last point we looked at
                if (gazeQueue.Count > 0)
                {
                    var inletPos = gazeQueue.Dequeue().pos;
                    lastPosition = Vector2.Scale(inletPos + surfToUnityOffset, dir);
                    return lastPosition;
                }

                return lastPosition;
            }
        }
        private void OnGaze(GazePosition gaze)
        {
            gazeQueue.Enqueue(gaze);
        }
    }
}

