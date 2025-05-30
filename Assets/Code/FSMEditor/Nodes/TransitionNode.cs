using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class TransitionNode : Node
{
	public int m_EnterNode;
	public int m_TargetNode;

	[HideInInspector]	
	public Condition m_PreviousCondition;
	public Condition m_Condition;
	public int m_TransitionId;

	public override void DrawWindow()
	{
		StateNode l_EnterNode=FSMEditor.m_Settings.m_CurrentGraph.GetStateNodeWithIndex(m_EnterNode);
		if(l_EnterNode==null)
			return;

		Transition l_Transition=l_EnterNode.GetTransition(m_TransitionId);
		if(l_Transition==null)
			return;
		
		GUILayout.Space(10.0f); 

		if(l_Transition.m_Condition==null)
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
			{ 
				EditorGUILayout.LabelField("Condition");
				Node l_TargetNode=FSMEditor.m_Settings.m_CurrentGraph.GetStateNodeWithIndex(m_TargetNode);
				if(l_TargetNode!=null)
				{
					if(!l_TargetNode.m_AlreadyExists)
						l_Transition.m_TargetState=l_TargetNode.m_Id;
					else
						l_Transition.m_TargetState=-1;
				}
				else
					l_Transition.m_TargetState=-1;
			}
		}

		l_Transition.m_Condition=(Condition)EditorGUILayout.ObjectField(l_Transition.m_Condition, typeof(Condition), false);
		m_Condition=l_Transition.m_Condition;

		if(m_PreviousCondition!=l_Transition.m_Condition)
		{
			m_PreviousCondition=l_Transition.m_Condition;
			m_AlreadyExists=FSMEditor.m_Settings.m_CurrentGraph.DoesTransitionAlreadyExist(this);
		}
	}
	public void DrawLine()
	{
		Rect l_Rect=m_WindowRect;
		l_Rect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
		l_Rect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;

		StateNode l_EnterNode=FSMEditor.m_Settings.m_CurrentGraph.GetStateNodeWithIndex(m_EnterNode);
		if(l_EnterNode==null)
			FSMEditor.m_Settings.m_CurrentGraph.DeleteTransitionNode(m_Id);
		else
		{
			Color l_TargetColor=Color.green;
			if(!m_Assigned || m_AlreadyExists || l_EnterNode.m_OnStateAction==null)
				l_TargetColor=Color.red;

			Rect l_PreviousRect=l_EnterNode.m_WindowRect;
			l_PreviousRect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
			l_PreviousRect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;

			FSMEditor.DrawNodeLine(l_PreviousRect, l_Rect, l_TargetColor);
		}
		
		if(m_AlreadyExists || m_TargetNode==-1)
			return;

		Node l_TargetNode=FSMEditor.m_Settings.m_CurrentGraph.GetStateNodeWithIndex(m_TargetNode);
		if(l_TargetNode==null)
			m_TargetNode=-1;
		else
		{
			Transition l_Transition=l_EnterNode.GetTransition(m_TransitionId);

			Rect l_NextRect=l_TargetNode.m_WindowRect;
			l_NextRect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
			l_NextRect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;

			Color l_TargetColor=Color.green;
			if(l_TargetNode is StateNode _StateNode)
			{
				if(!_StateNode.m_Assigned || _StateNode.m_AlreadyExists || l_Transition.m_Condition==null)
					l_TargetColor=Color.red;
			}
			else
			{
				if(!l_TargetNode.m_Assigned)
					l_TargetColor=Color.red;
				else
					l_TargetColor=Color.cyan;
			}

			FSMEditor.DrawNodeLine(l_Rect, l_NextRect, l_TargetColor);
		}
	}
    public override void DrawLabel()
    {
        GUI.Box(new Rect(m_WindowRect.x, m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight),
			"TRANSITION", FSMEditor.m_Settings.GetTransitionNodeLabelStyle());
    }
}
