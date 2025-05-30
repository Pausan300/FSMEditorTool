using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class StateNode : Node
{
	public StateAction m_OnStateAction;
	public StateAction m_OnEnterAction;
	public StateAction m_OnExitAction;

	public List<int> m_Transitions=new List<int>();

	public bool m_IsEntryNode;
	

	public void CheckTransitions(StateManager States)
	{
		for(int i=0; i<m_Transitions.Count; ++i)
		{
			TransitionNode l_Transition=States.m_AssignedGraph.GetTransitionNodeWithIndex(m_Transitions[i]);
			if(l_Transition.m_TargetNodeId!=-1 && l_Transition.m_Condition.CheckCondition(States, States.GetBlackboard()))
			{
				States.SetCurrentState(l_Transition.m_TargetNodeId);
				return;
			}
		}
	}
	public void AddTransition(int Id)
	{
		m_Transitions.Add(Id);
	}
	public void RemoveTransition(int Id) 
	{
		for(int i=0; i<m_Transitions.Count; ++i)
		{
			if(m_Transitions[i]==Id)
				m_Transitions.Remove(m_Transitions[i]);
		}
	}

	public override void DrawWindow()
	{
		EditorGUILayout.LabelField(m_WindowTitle, FSMEditor.m_Settings.GetCenteredTextStyle());
		GUILayout.Space(10.0f); 

		if(m_OnEnterAction==null)
		{
			EditorGUILayout.LabelField("No OnEnter assigned");
		}
		else 
		{
			EditorGUILayout.LabelField("OnEnter Action");
		}
		m_OnEnterAction=(StateAction)EditorGUILayout.ObjectField(m_OnEnterAction, typeof(StateAction), false);
		GUILayout.Space(10.0f);

		if(m_OnStateAction==null)
		{
			EditorGUILayout.LabelField("ONSTATE ACTION NEEDED", FSMEditor.m_Settings.GetRedTextStyle());
		}
		else 
		{
			EditorGUILayout.LabelField("OnState Action");
		}
		m_OnStateAction=(StateAction)EditorGUILayout.ObjectField(m_OnStateAction, typeof(StateAction), false);
		GUILayout.Space(10.0f);	
		
		if(m_OnExitAction==null)
		{
			EditorGUILayout.LabelField("No OnExit assigned");
		}
		else 
		{
			EditorGUILayout.LabelField("OnExit Action");
		}
		m_OnExitAction=(StateAction)EditorGUILayout.ObjectField(m_OnExitAction, typeof(StateAction), false);
		GUILayout.Space(10.0f);	

		if(m_OnStateAction!=null)
			m_Assigned=true;
		else
			m_Assigned=false;
	}
	public override void DrawLabel()
    {
		GUI.Box(new Rect(m_WindowRect.x, m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight), 
			"STATE", FSMEditor.m_Settings.GetStateNodeLabelStyle());
		if(m_IsEntryNode)
			EditorGUI.LabelField(new Rect(m_WindowRect.x+m_WindowRect.width/3.0f, m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight), "(ENTRY STATE)");
	}
}
