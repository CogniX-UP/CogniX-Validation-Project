using Cognix.LSL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public class EyeGazeInput : BasePuzzleInput
    {
        [SerializeField] RectTransform rect;
        [SerializeField] PupilCoreInlet eyeInlet;
        [SerializeField] Vector2 dir = Vector2.one;
        [SerializeField] Vector2 surfToUnityOffset = Vector2.zero;

        Queue<GazePosition> gazeQueue = new Queue<GazePosition>();
        [SerializeField] Vector2 inletPos = Vector2.zero;
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
                    inletPos = gazeQueue.Dequeue().pos;
                    lastPosition = Vector2.Scale(inletPos + surfToUnityOffset, dir);
                }
                return lastPosition;
            }
        }

        private void OnGaze(GazePosition gaze)
        {
            gazeQueue.Enqueue(gaze);
        }

        public override Puzzle.Direction Direction
        {
            get
            {
                if (Input.GetKeyDown(KeyCode.W))
                    return Puzzle.Direction.Up;
                else if (Input.GetKeyDown(KeyCode.S))
                    return Puzzle.Direction.Down;
                else if (Input.GetKeyDown(KeyCode.D))
                    return Puzzle.Direction.Right;
                else if (Input.GetKeyDown(KeyCode.A))
                    return Puzzle.Direction.Left;

                return Puzzle.Direction.None;
            }
        }
    }
}

