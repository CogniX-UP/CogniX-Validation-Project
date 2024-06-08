using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cognix.LSL
{
    [System.Serializable]
    public class ChannelMap: ISerializationCallbackReceiver
    {
        [SerializeField] List<string> channelNames = new();

        Dictionary<string, int> channelToIndex = new Dictionary<string, int>();

        public IReadOnlyList<string> ChannelNames => channelNames;
        public IReadOnlyDictionary<string, int> ChannelToIndex => channelToIndex;

        public void SetChannelNames(List<string> labels)
        {
            this.channelNames = labels;
            ((ISerializationCallbackReceiver)this).OnAfterDeserialize();
        }
        public void SetChannelNames(IEnumerable<string> labels)
        {
            var newChannels = new List<string>(labels);
            SetChannelNames(newChannels);
        }
        public bool AddChannel(string label)
        {
            if (string.IsNullOrEmpty(label) || channelToIndex.ContainsKey(label))
                return false;

            channelNames.Add(label);
            channelToIndex.Add(channelNames[-1], channelNames.Count-1);
            return true;
        }
        public bool RemoveChannel(string label)
        {
            if (string.IsNullOrEmpty(label) || !channelToIndex.ContainsKey(label))
                return false;

            channelNames.Remove(label);
            channelToIndex.Remove(label);
            return true;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {

        }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            channelToIndex.Clear();
            for (int i = 0; i < channelNames.Count; i++)
            {
                var label = channelNames[i];
                channelToIndex[label] = i; 
            }
        }
    }
}

