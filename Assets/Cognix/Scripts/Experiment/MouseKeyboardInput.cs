using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public class MouseKeyboardInput : BasePuzzleInput
    {
        [SerializeField] RectTransform rect;

        Camera cam;
        private void Awake()
        {
            cam = Camera.main;
        }
        public override Vector2 Point
        {
            get
            {
                var mouse = Input.mousePosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, mouse, cam, out var localPoint);
                // We're assuming the pivot is in the center, if it isn't, it should be changed in the editor
                var r = rect.rect;
                // Transforming the local point to (-0.5, 0.5) => (0, 0)
                localPoint /= new Vector2(r.width, r.height);
                localPoint += new Vector2(0.5f, -0.5f);
                localPoint.y *= -1;
                return localPoint;
            }
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

