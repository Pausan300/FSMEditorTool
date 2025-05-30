using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
	public Graph m_AssignedGraph;
	StateNode m_CurrentState;
	Blackboard m_Blackboard;

	private void Start()
	{
		m_Blackboard=transform.GetComponent<Blackboard>();
		if(m_AssignedGraph!=null) 
		{
			m_CurrentState=m_AssignedGraph.GetEntryNode();
		}
	}
	private void Update()
	{
		if(m_AssignedGraph!=null) 
		{
			m_CurrentState.m_OnStateAction.Execute(this, m_Blackboard);
			m_CurrentState.CheckTransitions(this);
		}
	}
	public Blackboard GetBlackboard()
	{
		return m_Blackboard;
	}

	public Node GetCurrentState() 
	{
		return m_CurrentState;
	}
	public void SetCurrentState(int Index) 
	{
		if(m_CurrentState.m_OnExitAction!=null)
			m_CurrentState.m_OnExitAction.Execute(this, m_Blackboard);
		m_CurrentState=m_AssignedGraph.GetStateNodeWithIndex(Index);
		if(m_CurrentState.m_OnEnterAction!=null)
			m_CurrentState.m_OnEnterAction.Execute(this, m_Blackboard);
	}
}
