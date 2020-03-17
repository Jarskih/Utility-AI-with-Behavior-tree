using BehaviorTrees;
using XNode;
using UnityEngine;

public abstract class BehaviorTreeNode : Node
{
    [Output] public BehaviorTreeResult outResult;
    public BehaviorTreeAgent agent;
    public string nodeDescription = "";

    public abstract BehaviorTreeResult Execute();

    public virtual void OnInit() { }

    public override object GetValue(NodePort port)
    {
        if (!Application.isPlaying || port?.Connection == null) { return BehaviorTreeResult.Failure; }

        BehaviorTreeNode parentNode = port.Connection.node as BehaviorTreeNode;

        if ( parentNode != null)
        {
            agent = parentNode.agent;
            //Debug.Log("From " + parentNode.GetType() + " " + parentNode.nodeDescription + " To " + GetType() + " " + this.nodeDescription);
        }

        if (agent.Owner == null)
        {
            return BehaviorTreeResult.Failure;
        }
        return Execute();
    }
}
