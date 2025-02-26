﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : PollingPool<AudioSource>
{
    public AudioSourcePool(AudioSource prefab) : base(prefab)
    {
    }

    public AudioSource GetAudioSource()
    {
        return Get();
    }

    protected override bool IsActive(AudioSource component)
    {
        return component.isPlaying;
    }

    public void Play(AudioEvent simpleAudioEvent)
    {
        var source = Get();
        simpleAudioEvent.Play(source);
    }
}
