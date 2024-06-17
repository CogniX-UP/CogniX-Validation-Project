using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] BasePuzzleInput input;
        [SerializeField] List<PuzzleRow> initPieces = new List<PuzzleRow>();

        PuzzlePiece selectedPiece;

        public State PuzzleState { get; private set; } = State.Off;
        public PuzzlePiece Selected => selectedPiece;
        private void Awake()
        {
            // No sanity checking for now.. or ever
            var cols = initPieces.Count;
            var rows = initPieces[0].row.Count;

            var pieces = new PuzzlePiece[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    pieces[i, j] = initPieces[j].row[i];
                }
            }
            puzzle.Pieces = pieces;
        }

        [ContextMenu("Test Begin")]
        private void TestBeginGame()
        {
            StartCoroutine(puzzle.BeginGame(null));
            PuzzleState = State.Select;
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

