using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePoolManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private AudioSourcePool _pool;

    void Start()
    {
        _pool = new AudioSourcePool(prefab.GetComponent<AudioSource>());
    }

    public AudioSource GetAudioSource()
    {
        return _pool.GetAudioSource();
    }

    public void PlayAudioEvent(AudioEvent audioEvent)
    {
        _pool.Play(audioEvent);
    }
}
