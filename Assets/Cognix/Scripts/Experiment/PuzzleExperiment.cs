using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cognix.Validation
{
    public class PuzzleExperiment : MonoBehaviour
    {
        public enum State
        {
            Off, Select, Move
        }
        [System.Serializable]
        class PuzzleRow
        {
            public List<PuzzlePiece> row;
        }

        [SerializeField] Puzzle puzzle;
        [SerializeField] List<PuzzleRow> initPieces = new List<PuzzleRow>();
        [SerializeField] float winWaitTime = 1f;
        [SerializeField] GameObject winPanel;

        public UnityEvent onGameEnd;

        PuzzlePiece selectedPiece;

        public State PuzzleState { get; private set; } = State.Off;
        public PuzzlePiece Selected => selectedPiece;
        public Puzzle Puzzle => puzzle;
        private void Awake()
        {
            winPanel.SetActive(false);
            // No sanity checking for now.. or ever
            var cols = initPieces.Count;
            var rows = initPieces[0].row.Count;

            var pieces = new PuzzlePiece[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    pieces[i, j] = initPieces[i].row[j];
                }
            }
            puzzle.Pieces = pieces;
        }

        public void BeginGame()
        {
            StartCoroutine(puzzle.BeginGame(null));
            PuzzleState = State.Select;
        }

        public void MoveSelected(Puzzle.Direction dir)
        {
            PuzzleState = State.Move;
            IEnumerator move()
            {
                yield return puzzle.MovePiece(selectedPiece, dir);
                SelectPiece(null);
                if (puzzle.IsPuzzleCorrect())
                {
                    winPanel.SetActive(true);
                    yield return new WaitForSeconds(winWaitTime);
                    onGameEnd?.Invoke();
                    winPanel.SetActive(false);
                    gameObject.SetActive(false);
                    PuzzleState = State.Off;
                }
                else
                    PuzzleState = State.Select;
            }
            StartCoroutine(move());
        }
        // Selects a piece by a normalized position
        // (0,0) => top left
        // (1, 1) => bottom right
        public void SelectPieceByPos(Vector2 pos)
        {
            var piece = puzzle.PieceByNormPos(pos);
            SelectPiece(piece);
        }
        public void SelectPiece(PuzzlePiece piece)
        {
            if (piece == selectedPiece)
                return;
            if (selectedPiece != null)
                selectedPiece.SetDefaultColor();

            selectedPiece = null;
            if (piece == null)
                return;

            piece.SetSelectColor();
            selectedPiece = piece;
        }
    }
}

