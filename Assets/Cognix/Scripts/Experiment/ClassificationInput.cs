using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cognix.Validation
{
    public class ClassificationInput : PuzzleDirectionInput
    {
        [SerializeField] ClassificationInlet classInlet;
        [SerializeField] Image leftProbIm, rightProbIm;
        [SerializeField] float probThreshold = 0.65f;
        public override Puzzle.Direction Direction
        {
            get
            {
                var leftProb = classInlet.leftProbability;
                var rightProb = classInlet.rightProbability;

                if (leftProb >= probThreshold)
                    return Puzzle.Direction.Left;
                else if (rightProb >= probThreshold)
                    return Puzzle.Direction.Right;
                else
                    return Puzzle.Direction.None;
            }
        }
        public void Update()
        {
            var leftProb = classInlet.leftProbability;
            var rightProb = classInlet.rightProbability;

            leftProbIm.fillAmount = leftProb;
            rightProbIm.fillAmount = rightProb;
        }
    }
}

