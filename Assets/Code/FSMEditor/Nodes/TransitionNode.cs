using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class TransitionNode : Node
{
	public int m_EnterNodeId=-1;
	public int m_TargetNodeId=-1;

	[HideInInspector]
	public Condition m_Condition;

	public override void DrawWindow()
	{
		if(m_EnterNodeId==-1)
			return;
		
		GUILayout.Space(10.0f); 

		if(m_Condition==null)
		{
			EditorGUILayout.LabelField("No Condition assigned");
			m_Assigned=false;
		}
		else
		{
			m_Assigned=true;
			if(m_AlreadyExists)
				EditorGUILayout.LabelField("Condition already exists");
			else
				EditorGUILayout.LabelField("Condition");
		}

		m_Condition=(Condition)EditorGUILayout.ObjectField(m_Condition, typeof(Condition), false);
	}
	public void DrawLine()
	{
		Rect l_OwnRect=m_WindowRect;
		l_OwnRect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
		l_OwnRect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;

		StateNode l_EnterNode=FSMEditor.m_Settings.m_CurrentGraph.GetStateNodeWithIndex(m_EnterNodeId);

		Color l_TargetColor=Color.green;
		if(!m_Assigned || m_AlreadyExists || !l_EnterNode.m_Assigned)
			l_TargetColor=Color.red;

		Rect l_PreviousRect = l_EnterNode.m_WindowRect;
		l_PreviousRect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
		l_PreviousRect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;

		FSMEditor.DrawNodeLine(l_PreviousRect, l_OwnRect, l_TargetColor);
		
		StateNode l_TargetNode=FSMEditor.m_Settings.m_CurrentGraph.GetStateNodeWithIndex(m_TargetNodeId);
		if(m_AlreadyExists || l_TargetNode==null)
			return;

		Rect l_NextRect = l_TargetNode.m_WindowRect;
		l_NextRect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
		l_NextRect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;

		l_TargetColor=Color.green;
		if(!l_TargetNode.m_Assigned || m_Condition==null)
			l_TargetColor=Color.red;

		FSMEditor.DrawNodeLine(l_OwnRect, l_NextRect, l_TargetColor);
	}
    public override void DrawLabel()
    {
        GUI.Box(new Rect(m_WindowRect.x, m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight),
			"TRANSITION", FSMEditor.m_Settings.GetTransitionNodeLabelStyle());
    }
}
