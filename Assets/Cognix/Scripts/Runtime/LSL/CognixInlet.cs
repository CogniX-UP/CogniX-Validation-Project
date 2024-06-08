using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL4Unity.Utils;
using LSL;
using System;

namespace Cognix.LSL
{
    public abstract class CognixInlet<T> : ABaseInlet<T>
    {
        [NonSerialized] double maxChunkDur = 0.2f;

        [SerializeField] double irregSamplingRate = 256;
        [SerializeField] protected ChannelMap channelMap;
        protected override void OnStreamAvailable() =>
            channelMap.SetChannelNames(ChannelNames);
        protected override void OnStreamLost() =>
            channelMap.SetChannelNames(new List<string>());

        public T SampleChannel(string channelName, T[] array)
        {
            var index = channelMap.ChannelToIndex[channelName];
            return array[index];
        }

        public override void AStreamIsFound(StreamInfo stream_info)
        {
            if (!isTheExpected(stream_info))
                return;

            Debug.Log(string.Format("LSL Stream {0} found for {1}", stream_info.name(), name));

            inlet = new StreamInlet(stream_info);
            var srate = inlet.info().nominal_srate();
            if (srate <= 0)
                srate = irregSamplingRate;
            int buf_samples = (int)Mathf.Ceil((float)(srate * maxChunkDur));
            int nChannels = inlet.info().channel_count();

            timestamp_buffer = new double[buf_samples];
            data_buffer = new T[buf_samples, nChannels];
            single_sample = new T[nChannels];

            XMLElement channels = inlet.info().desc().child("channels");
            XMLElement chan;
            if (!channels.empty())
                chan = channels.first_child();
            else
                chan = channels;

            ChannelNames.Clear();
            for (int chan_ix = 0; chan_ix < nChannels; chan_ix++)
            {
                if (!chan.empty())
                {
                    ChannelNames.Add(chan.child_value("label"));
                    chan = chan.next_sibling();
                }
                else
                    // Pad with empty strings to make ChannelNames length == nChannels.
                    ChannelNames.Add("Chan" + chan_ix);
            }

            OnStreamAvailable();
        }
    }
    public abstract class CognixShortInlet: CognixInlet<short>
    {
        protected override void pullChunk()
            => ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
    }
    public abstract class CognixIntInlet: CognixInlet<int>
    {
        protected override void pullChunk()
            => ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
    }
    public abstract class CognixFloatInlet: CognixInlet<float>
    {
        protected override void pullChunk()
            => ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
    }
    public abstract class CognixDoubleInlet : CognixInlet<double>
    {
        protected override void pullChunk()
            => ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
    }
    public abstract class CognixCharInlet : CognixInlet<char>
    {
        protected override void pullChunk()
            => ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
    }
    public abstract class CognixStringInlet : CognixInlet<string>
    {
        protected override void pullChunk()
            => ProcessChunk(inlet.pull_chunk(data_buffer, timestamp_buffer));
    }
}

