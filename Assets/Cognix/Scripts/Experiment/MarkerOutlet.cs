using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL4Unity.Utils;

namespace Cognix.Validation {
    public class MarkerOutlet : AStringOutlet
    {
        [SerializeField]
        string channelName = "Trial Channel";
        List<string> channelNames;
        private string marker = null;

        private void Awake()
        {
            channelNames = new() { channelName };
        }
        public override List<string> ChannelNames => channelNames;
        protected override bool BuildSample()
        {
            if (string.IsNullOrEmpty(marker))
                return false;

            sample[0] = marker;
            marker = null;
            return true;
        }

        /// <summary>
        /// Writes a marker to be sent to the stream
        /// If this happens on multiple frames, the last marker will be accepted.
        /// </summary>
        /// <param name="marker"></param>
        public void WriteMarker(string marker) => this.marker = marker;

#if UNITY_EDITOR
        [SerializeField] string testMarker = "NewTrial";

        [ContextMenu("Test")]
        private void Test()
        {
            WriteMarker(testMarker);
        }
#endif
    }
}

