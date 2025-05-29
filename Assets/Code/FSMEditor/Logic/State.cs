using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class State : ScriptableObject
{
	public StateAction m_OnState;
	public List<Transition> m_Transitions=new List<Transition>();

	public bool m_CurrentlyExecuting;

    public void OnState(StateManager States)
	{
		ExecuteActions(States, m_OnState);
		CheckTransitions(States);
	}
	public void CheckTransitions(StateManager States)
	{
		for(int i=0; i<m_Transitions.Count; ++i)
		{
			if(m_Transitions[i].m_Disable)
				continue;

			if(m_Transitions[i].m_Condition.CheckCondition(States, States.GetBlackboard()))
			{
				//if(m_Transitions[i].m_TargetState!=null)
				//{
				//	States.m_CurrentState.m_CurrentlyExecuting=false;
				//	States.m_CurrentState=m_Transitions[i].m_TargetState;
				//	States.m_CurrentState.m_CurrentlyExecuting=true;
				//}
				return;
			}
			return;
		}
	}
	public void ExecuteActions(StateManager States, StateAction Action)
	{
		if(Action!=null)
			Action.Execute(States, States.GetBlackboard());
	}
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
