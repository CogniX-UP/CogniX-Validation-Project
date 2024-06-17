using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public class Puzzle : MonoBehaviour
    {
        PuzzlePiece[,] pieces = new PuzzlePiece[2, 2];
        private PuzzlePiece[,] state;
        public PuzzlePiece[,] Pieces 
        { 
            get => pieces;
            set
            {
                pieces = value;
                for (int i = 0; i < pieces.GetLength(0); i++)
                {
                    for (int j = 0; j < pieces.GetLength(1); j++)
                    {
                        var piece = pieces[i, j];
                        piece.currentIndices = piece.correctIndices = (i,j);
                        piece.correctLocalPos = piece.transform.localPosition;
                    }
                }
            } 
        }
        [field: SerializeField] public float Transition { get; set; } = 1.25f;
        
        public IEnumerator BeginGame(PuzzlePiece[,] state)
        {
            if (state != null && 
                state.Length == Pieces.Length && 
                state.LongLength == pieces.LongLength)
            {
                this.state = state;
            }
            else
            {
                Randomize();
            }

            foreach (var piece in pieces)
                MoveToCurrentPosition(piece);

            if (Transition > 0)
                yield return new WaitForSeconds(Transition);
        }


        private void Randomize()
        {
            var shuffleList = new List<PuzzlePiece>();
            var rows = Pieces.GetLength(0);
            var cols = Pieces.GetLength(1);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    shuffleList.Add(Pieces[i, j]);

            // Fisher-Yates
            var n = shuffleList.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                var val = shuffleList[k];
                shuffleList[k] = shuffleList[n];
                shuffleList[n] = val;
            }

            state = new PuzzlePiece[rows, cols];
            var index = 0;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    state[i, j] = shuffleList[index];
                    state[i, j].currentIndices = (i, j);
                    index++;
                }
        }

        private void MoveToCurrentPosition(PuzzlePiece piece)
        {
            (int i, int j) = piece.currentIndices;
            var correctPiece = pieces[i, j];
            piece.transform.DOLocalMove(correctPiece.correctLocalPos, Transition);
        }
    }
}

