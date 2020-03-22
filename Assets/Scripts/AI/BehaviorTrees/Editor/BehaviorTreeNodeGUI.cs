using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using XNodeEditor;

/*

namespace BehaviorTrees.Editor
{
    [CustomNodeEditor(typeof(BehaviorTreeNode))]
    public class BehaviorTreeNodeGUI : NodeEditor
    {
        public override void OnHeaderGUI()
        {
            GUI.color = Color.white;
            BehaviorTreeNode node = target as BehaviorTreeNode;
            BehaviorTreeGraph graph = node.graph as BehaviorTreeGraph;
            if (node.outResult == BehaviorTreeResult.Success) GUI.color = Color.green;
            string title = target.name;
            GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
            GUI.color = Color.white;
        }
    }
}

*/