using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public class KeyboardDirectionInput : PuzzleDirectionInput
    {
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