using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cognix.Validation
{
    public class ClassificationInput : PuzzleDirectionInput
    {
        [SerializeField] ClassificationInlet classInlet;
        [SerializeField] Image horizontalProbIm, verticalProbIm;
        [SerializeField] float probThreshold = 0.65f;

        private float HorizontalProb => classInlet.rightProbability;
        private float VerticalProb => classInlet.leftProbability;
        public override Puzzle.Direction Direction
        {
            get
            {
                var horProb = HorizontalProb;
                var verProb = VerticalProb;

                if (horProb >= probThreshold)
                    return Puzzle.Direction.Right;
                else if (verProb >= probThreshold)
                    return Puzzle.Direction.Down;
                else
                    return Puzzle.Direction.None;
            }
        }
        public void Update()
        {
            var leftProb = HorizontalProb;
            var rightProb = VerticalProb;

            horizontalProbIm.fillAmount = leftProb;
            verticalProbIm.fillAmount = rightProb;
        }
    }
}

