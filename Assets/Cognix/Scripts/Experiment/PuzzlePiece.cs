using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Cognix.Validation
{
    public class PuzzlePiece : MonoBehaviour
    {
        [SerializeField] Image background;
        [SerializeField] TMP_Text text;
        [SerializeField] Color defaultColor;
        [SerializeField] Color selectColor;
        [SerializeField] float transition = 0.25f;
        public TMP_Text TMP => text;
        public Vector3 correctLocalPos; // represents the correct position inside the puzzle
        public (int, int) correctIndices;
        public (int, int) currentIndices;
        
        public void SetColor(Color color, float transition = -1)
        {
            if (background.color == color)
                return;
            if (transition < 0)
                transition = this.transition;

            if (transition == 0 || !Application.isPlaying)
            {
                background.color = color;
                text.color = color;
            }
            else
            {
                background.DOColor(color, transition);
                text.DOColor(color, transition);
            }
        }

        public void SetDefaultColor(float transition = - 1) => SetColor(defaultColor, transition);
        public void SetSelectColor(float transition = -1) => SetColor(selectColor, transition);

        [ContextMenu("Test Default Color")]
        private void TestDefaultColor() => SetDefaultColor();
        [ContextMenu("Test Select Color")]
        private void TestSelectColor() => SetSelectColor();
    }
}

