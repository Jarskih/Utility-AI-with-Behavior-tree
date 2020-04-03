using AI;
using BehaviorTrees;
using UnityEngine;

[Leaf(typeof(PlaySoundNode))]
public class PlaySoundNode : BehaviorTreeNode
{
    public AudioEvent _audio;

    public override BehaviorTreeResult Execute()
    {
        AudioSourcePoolManager poolManager = FindObjectOfType<AudioSourcePoolManager>();
        poolManager.PlayAudioEvent(_audio);

        return BehaviorTreeResult.Success;
    }
}