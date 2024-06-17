using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public class Puzzle : MonoBehaviour
    {
        public enum Direction
        {
            None, Left, Up, Right, Down
        }
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
        
        public PuzzlePiece PieceByNormPos(Vector2 pos)
        {
            var x = Mathf.Clamp01(pos.x);
            var y = Mathf.Clamp01(pos.y);

            var indX = Mathf.FloorToInt(pieces.GetLength(0) * x);
            var indY = Mathf.FloorToInt(pieces.GetLength(1) * y);

            // Reversed
            return state[indY, indX];
        }
        public bool IsPuzzleCorrect()
        {
            var rows = pieces.GetLength(0);
            var cols = pieces.GetLength(1);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (pieces[i, j] != state[i, j])
                        return false;
            return true;
        }
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

            if (Transition == 0)
                return null;
            return WaitForTransition();
        }
        public IEnumerator MovePiece(PuzzlePiece piece, Direction dir)
        {
            (int newRow, int newCol) = piece.currentIndices;
            switch (dir)
            {
                case Direction.Left:
                    newCol--;
                    break;
                case Direction.Right:
                    newCol++;
                    break;
                case Direction.Down:
                    newRow++;
                    break;
                case Direction.Up:
                    newRow--;
                    break;
            }

            // We can only move once at a time, so no further logic is needed
            var rows = Pieces.GetLength(0);
            var cols = Pieces.GetLength(1);

            if (newRow < 0)
                newRow = rows-1;
            else if (newRow >= rows)
                newRow = 0;

            if (newCol < 0)
                newCol = cols - 1;
            else if (newCol >= cols)
                newCol = 0;

            (int oldRow, int oldCol) = piece.currentIndices;
            var swapPiece = state[newRow, newCol];

            state[newRow, newCol] = piece;
            piece.currentIndices = (newRow, newCol);
            
            state[oldRow, oldCol] = swapPiece;
            swapPiece.currentIndices = (oldRow, oldCol);

            MoveToCurrentPosition(piece);
            MoveToCurrentPosition(swapPiece);

            if (Transition == 0)
                return null;
            return WaitForTransition();
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
        private IEnumerator WaitForTransition()
        {
            yield return new WaitForSeconds(Transition);
        }
    }
}

