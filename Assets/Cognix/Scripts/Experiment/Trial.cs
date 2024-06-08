using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cognix.Validation
{
    public class Trial : MonoBehaviour
    {
        public Image image;
        public string markerName;
        public float trialDuration = 5;
        private WaitForSeconds TrialWaiter => new WaitForSeconds(trialDuration);
        public void Show()
        {
            image.gameObject.SetActive(true);
        }
        public void Hide()
        {
            image.gameObject.SetActive(false);
        }
    }
}

