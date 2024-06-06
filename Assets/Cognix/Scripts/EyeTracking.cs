using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL4Unity;
using LSL4Unity.Utils;

public class EyeTracking : AFloatInlet
{

    protected override void OnStreamAvailable()
    {
        Debug.Log(ChannelCount, this);
        foreach (var label in ChannelNames)
            Debug.Log(label);
    }
    protected override void OnStreamLost()
    {
        Debug.Log("On Stream Lost");
    }
    protected override void pullChunk()
    {
        base.pullChunk();
    }
    protected override void Process(float[] newSample, double timestamp)
    {
        Debug.Log("Someone");
    }
}
