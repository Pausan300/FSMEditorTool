using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
	public Graph m_AssignedGraph;
	//public State m_CurrentState;
	Node m_CurrentState;
	public Transform m_Transform;
	Blackboard m_Blackboard;

	private void Start()
	{
		m_Transform=this.transform;
		m_Blackboard=transform.GetComponent<Blackboard>();
		if(m_AssignedGraph!=null) 
		{
			m_CurrentState=m_AssignedGraph.GetEntryNode();
		}
	}
	private void Update()
	{
		//if(m_CurrentState!=null)
		//	m_CurrentState.OnState(this);
		if(m_AssignedGraph!=null) 
		{
			m_CurrentState.m_StateReferences.m_OnStateAction.Execute(this, m_Blackboard);
			m_CurrentState.m_StateReferences.CheckTransitions(this);
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
		if(m_CurrentState.m_StateReferences.m_OnExitAction!=null)
			m_CurrentState.m_StateReferences.m_OnExitAction.Execute(this, m_Blackboard);
		m_CurrentState=m_AssignedGraph.GetNodeWithIndex(Index);
		if(m_CurrentState.m_StateReferences.m_OnEnterAction!=null)
			m_CurrentState.m_StateReferences.m_OnEnterAction.Execute(this, m_Blackboard);
	}
}
