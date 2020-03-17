using BehaviorTrees;
using UnityEngine;

[Leaf(typeof(Wait))]
public class Wait : BehaviorTreeNode
{
    public float seconds;
    private float timer;
    public override BehaviorTreeResult Execute()
    {
        if (timer > seconds)
        {
            timer = 0;
            return BehaviorTreeResult.Success;
        }
        
        timer += Time.deltaTime;
        return BehaviorTreeResult.Running;
    }
}