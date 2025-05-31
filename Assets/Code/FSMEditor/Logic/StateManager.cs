using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
	public Graph m_AssignedGraph;
	Graph m_GraphInstance;
	StateNode m_CurrentState;
	Blackboard m_Blackboard;

	private void Awake()
	{
		m_Blackboard=transform.GetComponent<Blackboard>();

		if(m_AssignedGraph!=null) 
		{
			m_GraphInstance=Instantiate(m_AssignedGraph);
			m_CurrentState=m_GraphInstance.GetEntryNode();
			m_CurrentState.m_IsCurrentlyPlaying=true;
		}
	}
	private void Update()
	{
		if(m_GraphInstance!=null) 
		{
			m_CurrentState.m_OnStateAction.Execute(this, m_Blackboard);
			m_CurrentState.CheckTransitions(this);
		}
	}
	public Blackboard GetBlackboard()
	{
		return m_Blackboard;
	}
	public Graph GetGraph() 
	{
		return m_GraphInstance;
	}

	public Node GetCurrentState() 
	{
		return m_CurrentState;
	}
	public void SetCurrentState(int Index) 
	{
		if(m_CurrentState.m_OnExitAction!=null)
			m_CurrentState.m_OnExitAction.Execute(this, m_Blackboard);
		m_CurrentState.m_IsCurrentlyPlaying=false;
		m_CurrentState=m_GraphInstance.GetStateNodeWithIndex(Index);
		m_CurrentState.m_IsCurrentlyPlaying=true;
		if(m_CurrentState.m_OnEnterAction!=null)
			m_CurrentState.m_OnEnterAction.Execute(this, m_Blackboard);
	}
}
