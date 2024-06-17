using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cognix.Validation
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] Button startGameButton;
        [Header("Other")]
        [SerializeField] PuzzleExperiment experiment;

        private void Awake()
        {
            startGameButton.onClick.AddListener(BeginGame);
        }
        public void BeginGame()
        {
            experiment.gameObject.SetActive(true);
            experiment.BeginGame();
            this.gameObject.SetActive(false);
        }

        public void EndGame()
        {
            this.gameObject.SetActive(true);
        }

    }
}

