using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.Validation
{
    public class PuzzleExperiment : MonoBehaviour
    {
        [System.Serializable]
        class PuzzleRow
        {
            public List<PuzzlePiece> row;
        }

        [SerializeField] Puzzle puzzle;
        [SerializeField] List<PuzzleRow> initPieces = new List<PuzzleRow>();

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
        }
    }
}

