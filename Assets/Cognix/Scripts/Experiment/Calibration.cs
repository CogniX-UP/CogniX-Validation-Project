using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cognix.Validation
{
    public class Calibration : MonoBehaviour
    {

        public bool playOnAwake;
        [Header("Trial Settings")]
        public int numTrials = 20;
        public Vector2 timeBetweenTrials = new Vector2(1.5f, 3.5f);
        public float waitBeforeTrial = 3;
        public Image crossImage;
        public Trial leftTrial;
        public Trial rightTrial;

        [Header("LSL")]
        public MarkerOutlet markerOutlet;

        public Action onCalibEnd;
        WaitForSeconds beforeTrialWaiter;
        Dictionary<Trial, int> remainingTrials = new Dictionary<Trial, int>();
        WaitForEndOfFrame frameWaiter = new WaitForEndOfFrame();

        private void Start()
        {
            if (playOnAwake)
                StartCoroutine(BeginTrial());
        }
        public void StartCalibration()
        {
            gameObject.SetActive(true);
            StartCoroutine(BeginTrial());
        }
        public IEnumerator BeginTrial() => BeginTrial(numTrials);
        public IEnumerator BeginTrial(int numTrials)
        {
            gameObject.SetActive(true);
            beforeTrialWaiter = new WaitForSeconds(waitBeforeTrial);
            remainingTrials.Clear();

            IEnumerator beginTrial()
            {
                // Make sure the trials are an even number
                numTrials = this.numTrials;
                if (numTrials % 2 != 0)
                    numTrials += 1;

                remainingTrials[leftTrial] = numTrials / 2;
                remainingTrials[rightTrial] = numTrials / 2;

                int currentTrialNum = 0;

                while (currentTrialNum < numTrials)
                {
                    // Wait for a random amount of time
                    float waitTime = UnityEngine.Random.Range(timeBetweenTrials.x, timeBetweenTrials.y);
                    yield return new WaitForSeconds(waitTime);

                    // Show cross and wait x seconds
                    crossImage.gameObject.SetActive(true);
                    yield return beforeTrialWaiter;
                    crossImage.gameObject.SetActive(false);
                    // Choose trial, show it, then send marker
                    var trial = ChooseTrial();
                    trial.Show();
                    yield return frameWaiter;
                    markerOutlet.WriteMarker(trial.markerName);
                    yield return new WaitForSeconds(trial.trialDuration);
                    trial.Hide();
                    currentTrialNum += 1;
                }

                EndTrial();
                onCalibEnd?.Invoke();
            }

            return beginTrial();
        }
        public void EndTrial()
        {
            gameObject.SetActive(false);
            crossImage.gameObject.SetActive(false);
            leftTrial.Hide();
            rightTrial.Hide();
        }

        private Trial ChooseTrial()
        {
            var remainingLeft = remainingTrials[leftTrial];
            var remainingRight = remainingTrials[rightTrial];

            if (remainingLeft > 0 && remainingRight > 0)
            {
                int trial = UnityEngine.Random.Range(0, 2);
                // Left
                if (trial == 0)
                {
                    remainingTrials[leftTrial] = remainingLeft - 1;
                    return leftTrial;
                }
                else
                {
                    remainingTrials[rightTrial] = remainingRight - 1;
                    return rightTrial;
                }
            }
            else if (remainingLeft > 0)
            {
                remainingTrials[leftTrial] = remainingLeft - 1;
                return leftTrial;
            }
            else
            {
                remainingTrials[rightTrial] = remainingRight - 1;
                return rightTrial;
            }
        }
    }
}

