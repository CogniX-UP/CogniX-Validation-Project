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
        [SerializeField] Button calibrateButton;
        
        [Header("Other")]
        [SerializeField] PuzzleExperiment experiment;
        [SerializeField] Calibration calibration;

        private void Awake()
        {
            startGameButton.onClick.AddListener(BeginGame);
            calibrateButton.onClick.AddListener(BeginCalibration);
            calibration.onCalibEnd += Open;
            experiment.onGameEnd.AddListener(Open);
        }
        public void BeginGame()
        {
            experiment.gameObject.SetActive(true);
            experiment.BeginGame();
            this.gameObject.SetActive(false);
        }
        public void BeginCalibration()
        {
            this.gameObject.SetActive(false);
            calibration.StartCalibration();
        }

        public void Open()
        {
            this.gameObject.SetActive(true);
        }

    }
}

