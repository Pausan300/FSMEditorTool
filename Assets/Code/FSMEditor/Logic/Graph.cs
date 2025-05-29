using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Graph : ScriptableObject
{
	int m_EntryNodeId;

	Node m_CurrentState;

	public List<Node> m_Windows=new List<Node>();

	public List<PointerNode> m_Pointers=new List<PointerNode>();

	List<int> m_IndexesToDelete=new List<int>();

	[SerializeField]
	public int m_Id;

	public Node GetNodeWithIndex(int Index) 
	{
		for(int i=0; i<m_Windows.Count; ++i)
		{
			if(m_Windows[i].m_Id==Index)
				return m_Windows[i];
		}
		return null;
	}
	public void DeleteWindows()
	{
		for(int i=0; i<m_IndexesToDelete.Count; ++i)
		{
			Node l_Node=GetNodeWithIndex(m_IndexesToDelete[i]);
			if(l_Node!=null)
				m_Windows.Remove(l_Node); 
		}
		m_IndexesToDelete.Clear();
	}
	public void DeleteNode(int Index)
	{
		if(!m_IndexesToDelete.Contains(Index))
			m_IndexesToDelete.Add(Index);

		Node l_Node=GetNodeWithIndex(Index);
		if(l_Node!=null)
			m_Windows.Remove(l_Node);
	}
	public bool DoesNodeAlreadyExist(Node Node)
	{
		for(int i=0; i<m_Windows.Count; ++i)
		{
			if(m_Windows[i].m_Id==Node.m_Id)
				continue;
			//if(m_Windows[i].m_StateReferences.m_CurrentState==Node.m_StateReferences.m_CurrentState && !m_Windows[i].m_AlreadyExists)
			if(m_Windows[i].m_StateReferences.m_OnStateAction==Node.m_StateReferences.m_OnStateAction && !m_Windows[i].m_AlreadyExists)
				return true;
		}
		return false;
	}
	public bool DoesTransitionAlreadyExist(Node Node)
	{
		Node l_EnterNode=GetNodeWithIndex(Node.m_EnterNode);
		if(l_EnterNode==null)
			return false;
		//for(int i=0; i<l_EnterNode.m_StateReferences.m_CurrentState.m_Transitions.Count; ++i)
		for(int i=0; i<l_EnterNode.m_StateReferences.m_Transitions.Count; ++i) 
		{
			//Transition l_Transition=l_EnterNode.m_StateReferences.m_CurrentState.m_Transitions[i];
			Transition l_Transition=l_EnterNode.m_StateReferences.m_Transitions[i];
			if(l_Transition.m_Condition==Node.m_TransitionReferences.m_PreviousCondition && Node.m_TransitionReferences.m_TransitionId!=l_Transition.m_Id)
				return true;
		}
		return false;
	}
	public void SetEntryNode(int Id) 
	{
		if(GetNodeWithIndex(m_EntryNodeId)!=null)
			GetNodeWithIndex(m_EntryNodeId).m_StateReferences.m_IsEntryNode=false; 
		m_EntryNodeId=Id;
		GetNodeWithIndex(m_EntryNodeId).m_StateReferences.m_IsEntryNode=true;
	}
	public Node GetEntryNode() 
	{
		return GetNodeWithIndex(m_EntryNodeId);
	}
	public Node GetCurrentState() 
	{
		return m_CurrentState;
	}
}



