using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Editor/Settings")]
public class EditorSettings : ScriptableObject
{
	public Graph m_CurrentGraph;
    public StateNode m_StateNode;
	public TransitionNode m_TransitionNode;
	//public PointerNode m_PointerNode;

	[Header("Style Settings")]
	public GUISkin m_EditorSkin;
	public float m_NodeLabelHeight;

	//public bool m_MakeTransition;
	
	//public Node AddNodeOnGraph(DrawNode Type, float Width, float Height, string Title, Vector3 Pos)
	//{
	//	Node l_Node=new Node();
	//	l_Node.m_DrawNode=Type;
	//	l_Node.m_WindowRect.width=Width;
	//	l_Node.m_WindowRect.height=Height;
	//	l_Node.m_WindowTitle=Title;
	//	l_Node.m_WindowRect.x=Pos.x;
	//	l_Node.m_WindowRect.y=Pos.y;
	//	//l_Node.m_Id=m_CurrentGraph.m_Windows.Count;
	//	l_Node.m_Id=m_CurrentGraph.m_Id;
	//	m_CurrentGraph.m_Id++;
	//	m_CurrentGraph.m_Windows.Add(l_Node);
	//	l_Node.m_TransitionReferences=new TransitionNodeReferences();
	//	l_Node.m_StateReferences=new StateNodeReferences();
	//	return l_Node;
	//}

	//public Node AddStateNodeOnGraph(float Width, float Height, string Title, Vector3 Pos)
	//{
	//	Node l_Node=new Node();
	//	l_Node.m_DrawNode=m_StateNode;
	//	l_Node.m_WindowRect.width=Width;
	//	l_Node.m_WindowRect.height=Height;
	//	l_Node.m_WindowTitle=Title;
	//	l_Node.m_WindowRect.x=Pos.x;
	//	l_Node.m_WindowRect.y=Pos.y;
	//	l_Node.m_Id=m_CurrentGraph.m_Windows.Count;
	//	m_CurrentGraph.m_Windows.Add(l_Node);
	//	l_Node.m_TransitionReferences=new TransitionNodeReferences();
	//	l_Node.m_StateReferences=new StateNodeReferences();
	//	return l_Node;
	//}
	//public Node AddTransitionNodeOnGraph(float Width, float Height, string Title, Vector3 Pos)
	//{
	//	Node l_Node=new Node();
	//	l_Node.m_DrawNode=m_TransitionNode;
	//	l_Node.m_WindowRect.width=Width;
	//	l_Node.m_WindowRect.height=Height;
	//	l_Node.m_WindowTitle=Title;
	//	l_Node.m_WindowRect.x=Pos.x;
	//	l_Node.m_WindowRect.y=Pos.y;
	//	l_Node.m_Id=m_CurrentGraph.m_Windows.Count;
	//	m_CurrentGraph.m_Windows.Add(l_Node);
	//	l_Node.m_TransitionReferences=new TransitionNodeReferences();
	//	l_Node.m_StateReferences=new StateNodeReferences();
	//	return l_Node;
	//}
	//public Node AddPointerNodeOnGraph(float Width, float Height, string Title, Vector3 Pos)
	//{
	//	Node l_Node=new Node();
	//	l_Node.m_DrawNode=m_PointerNode;
	//	l_Node.m_WindowRect.width=Width;
	//	l_Node.m_WindowRect.height=Height;
	//	l_Node.m_WindowTitle=Title;
	//	l_Node.m_WindowRect.x=Pos.x;
	//	l_Node.m_WindowRect.y=Pos.y;
	//	l_Node.m_Id=m_CurrentGraph.m_Windows.Count;
	//	m_CurrentGraph.m_Windows.Add(l_Node);
	//	l_Node.m_TransitionReferences=new TransitionNodeReferences();
	//	l_Node.m_StateReferences=new StateNodeReferences();
	//	return l_Node;
	//}

	public void Save() 
	{
		EditorUtility.SetDirty(this);
		EditorUtility.SetDirty(m_CurrentGraph);
	}

	public GUIStyle GetStateNodeLabelStyle() 
	{
		return m_EditorSkin.GetStyle("StateNodeLabel");
	}
	public GUIStyle GetTransitionNodeLabelStyle() 
	{
		return m_EditorSkin.GetStyle("TransitionNodeLabel");
	}
	public GUIStyle GetRedTextStyle() 
	{
		return m_EditorSkin.GetStyle("Label");
	}
}
