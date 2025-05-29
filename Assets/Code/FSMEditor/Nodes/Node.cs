using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class Node 
{
	public int m_Id;
	public DrawNode m_DrawNode;

	//public enum TNodeType 
	//{
	//	STATE,
	//	TRANSITION
	//}
	//public TNodeType m_NodeType;

    public Rect m_WindowRect;
	public string m_WindowTitle;

	public int m_EnterNode;
	public int m_TargetNode;
	public bool m_AlreadyExists;
	public bool m_Assigned;

	//public bool m_Collapse;
	//[HideInInspector]
	//public bool m_WasCollapse;

	[SerializeField]
	public StateNodeReferences m_StateReferences;
	[SerializeField]
	public TransitionNodeReferences m_TransitionReferences;

	public void DrawWindow()
	{
		if(m_DrawNode!=null)
		{
			m_DrawNode.DrawWindow(this);
		}
	}
	public void DrawLabel()
	{
		if(m_DrawNode!=null)
			m_DrawNode.DrawLabel(this);
	}
	public void DrawLine()
	{
		if(m_DrawNode!=null)
			m_DrawNode.DrawLine(this);
	}

	public virtual void DrawWindoww() { }
	public virtual void DrawLabell() { }
	//protected virtual void DrawWindoww() { }
}

[System.Serializable]
public class StateNodeReferences
{
	//public State m_CurrentState;
	//[HideInInspector]
	//public State m_PreviousState;
	//public SerializedObject m_SerializedState;
	//public ReorderableList m_OnStateList;
	public StateAction m_OnStateAction;
	public StateAction m_OnEnterAction;
	public StateAction m_OnExitAction;
	public List<Transition> m_Transitions=new List<Transition>();
	public bool m_IsEntryNode;
	
	public void CheckTransitions(StateManager States)
	{
		for(int i=0; i<m_Transitions.Count; ++i)
		{
			if(m_Transitions[i].m_Disable)
				continue;

			if(m_Transitions[i].m_Condition.CheckCondition(States, States.GetBlackboard()))
			{
				//if(m_Transitions[i].m_TargetState!=null)
				if(m_Transitions[i].m_TargetState!=-1)
				{
					//States.m_CurrentState.m_CurrentlyExecuting=false;
					States.SetCurrentState(m_Transitions[i].m_TargetState);
					//States.m_CurrentState.m_CurrentlyExecuting=true;
				}
				return;
			}
			return;
		}
	}
	//public void ExecuteActions(StateManager States, StateAction Action)
	//{
	//	if(Action!=null)
	//		Action.Execute(States, States.GetBlackboard());
	//}
	public Transition AddTransition()
	{
		Transition l_Transition=new Transition();
		m_Transitions.Add(l_Transition);
		return l_Transition;
	}

	public Transition GetTransition(int Id)
	{
		for(int i=0; i<m_Transitions.Count; ++i)
		{
			if(m_Transitions[i].m_Id==Id)
				return m_Transitions[i];
		}

		return null;
	}
	public void RemoveTransition(int Id)
	{
		for(int i=0; i<m_Transitions.Count; ++i)
		{
			if(m_Transitions[i].m_Id==Id)
				m_Transitions.Remove(m_Transitions[i]);
		}
	}
}

[System.Serializable]
public class TransitionNodeReferences
{
	[HideInInspector]	
	public Condition m_PreviousCondition;
	public Condition m_Condition;
	public int m_TransitionId;
}