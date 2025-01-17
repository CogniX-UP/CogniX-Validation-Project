using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public class PuzzleInput : MonoBehaviour
    {
        [SerializeField]
        protected PuzzleExperiment experiment;
        [SerializeField]
        float timeToSelect = 0.5f;
        [SerializeField]
        PuzzlePointer pointer;
        [SerializeField]
        PuzzleDirectionInput directionInput;

        private float currentTime = 0;

        PuzzlePiece hovering = null;

        protected void Update()
        {
            UpdateSelection();
            UpdateSwap();
        }
        private void UpdateSelection()
        {
            var eState = experiment.PuzzleState;
            switch (eState)
            {
                case PuzzleExperiment.State.Off:
                case PuzzleExperiment.State.Move:
                    currentTime = 0;
                    return;
            }

            var point = pointer.Point;
            if (point.x < 0 || point.x > 1 || point.y < 0 || point.y > 1)
            {
                currentTime = 0;
                experiment.SelectPiece(null);
                return;
            }

            var hovering = experiment.Puzzle.PieceByNormPos(point);
            if (hovering != this.hovering)
            {
                this.hovering = hovering;
                currentTime = Time.deltaTime;
                return;
            }

            currentTime += Time.deltaTime;
            if (currentTime > timeToSelect)
            {
                experiment.SelectPiece(this.hovering);
                currentTime = 0;
            }
        }
        private void UpdateSwap()
        {
            var eState = experiment.PuzzleState;
            switch (eState)
            {
                case PuzzleExperiment.State.Off:
                case PuzzleExperiment.State.Move:
                    return;
            }

            if (experiment.Selected == null)
                return;

            var dir = directionInput.Direction;
            if (dir == Puzzle.Direction.None)
                return;

            experiment.MoveSelected(dir);
        }
    }
}

