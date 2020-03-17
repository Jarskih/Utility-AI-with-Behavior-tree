using UnityEngine;
using XNodeEditor;

/*
namespace BehaviorTrees.Editor
{
    [CustomNodeEditor(typeof(BehaviorTreeNode))]
    public class BehaviorTreeNodeGUI : NodeEditor
    {
        public override void OnHeaderGUI()
        {
            BehaviorTreeNode node = target as BehaviorTreeNode;
            if (node.outResult == BehaviorTreeResult.Success) GUI.color = Color.green;
            if (node.outResult == BehaviorTreeResult.Failure) GUI.color = Color.red;
            if (node.outResult == BehaviorTreeResult.Running) GUI.color = Color.yellow;
            
            string title = target.name;
            GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
            GUI.color = Color.white;
        }

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            BehaviorTreeNode node = target as BehaviorTreeNode;
            BehaviorTreeGraph graph = node.graph as BehaviorTreeGraph;
        }
    }
}
*/