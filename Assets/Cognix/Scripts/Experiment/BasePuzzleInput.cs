using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public abstract class BasePuzzleInput : MonoBehaviour
    {
        [SerializeField]
        protected PuzzleExperiment experiment;
        [SerializeField]
        float timeToSelect = 1f;

        private float currentTime = 0;
        public abstract Vector2 Point { get; }
        public abstract Puzzle.Direction Direction { get; }

        protected void Update()
        {
            var eState = experiment.PuzzleState;
            if (eState == PuzzleExperiment.State.Off || eState == PuzzleExperiment.State.Move)
            {
                currentTime = 0;
                return;
            }

            var point = Point;
            if (point.x < 0 || point.x > 1 || point.y < 0 || point.y > 1)
            {
                currentTime = 0;
                experiment.SelectPiece(null);
                return;
            }

            currentTime += Time.deltaTime;
            if (currentTime > timeToSelect)
            {
                experiment.SelectPieceByPos(point);
                currentTime = 0;
            }
        }
    }
}

