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
        [SerializeField] bool singleThreshold;
        [SerializeField] float horizontalThreshold = 0.8f, verticalThreshold = 0.8f;

        private float HorizontalProb => classInlet.rightProbability;
        private float VerticalProb => classInlet.leftProbability;

        private void OnValidate()
        {
            if (singleThreshold)
                verticalThreshold = horizontalThreshold;
        }
        public override Puzzle.Direction Direction
        {
            get
            {
                var horProb = HorizontalProb;
                var verProb = VerticalProb;

                if (horProb >= horizontalThreshold)
                    return Puzzle.Direction.Right;
                else if (verProb >= verticalThreshold)
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

