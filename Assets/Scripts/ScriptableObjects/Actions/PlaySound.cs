using UnityEngine;
public class PlaySound : ScriptableAction
{
    public AudioEvent _event;
    public override void Execute(GameObject gameObject)
    {
        AudioSourcePoolManager poolManager = FindObjectOfType<AudioSourcePoolManager>();
        poolManager.PlayAudioEvent(_event);
    }
}