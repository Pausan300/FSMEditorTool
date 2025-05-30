using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Graph : ScriptableObject
{
	int m_EntryNodeId;

	public List<StateNode> m_Windows=new List<StateNode>();
	public List<TransitionNode> m_TransitionNodes=new List<TransitionNode>();
	public List<PointerNode> m_PointerNodes=new List<PointerNode>();

	[SerializeField]
	public int m_Id;

	public Node GetNodeWithIndex(int Index) 
	{
		for(int i=0; i<m_Windows.Count; ++i)
		{
			if(m_Windows[i].m_Id==Index)
				return m_Windows[i];
		}
		for(int i=0; i<m_TransitionNodes.Count; ++i)
		{
			if(m_TransitionNodes[i].m_Id==Index)
				return m_TransitionNodes[i];
		}

		return null;
	}
	public StateNode GetStateNodeWithIndex(int Index) 
	{
		for(int i=0; i<m_Windows.Count; ++i)
		{
			if(m_Windows[i].m_Id==Index)
				return m_Windows[i];
		}
		return null;
	}
	public TransitionNode GetTransitionNodeWithIndex(int Index) 
	{
		for(int i=0; i<m_TransitionNodes.Count; ++i)
		{
			if(m_TransitionNodes[i].m_Id==Index)
				return m_TransitionNodes[i];
		}
		return null;
	}
	public void DeleteStateNode(int Index)
	{
		StateNode l_Node=GetStateNodeWithIndex(Index);
		if(l_Node!=null)
			m_Windows.Remove(l_Node);
	}
	public void DeleteTransitionNode(int Index)
	{
		TransitionNode l_Node=GetTransitionNodeWithIndex(Index);
		if(l_Node!=null)
			m_TransitionNodes.Remove(l_Node);
	}
	public bool DoesNodeAlreadyExist(StateNode Node)
	{
		for(int i=0; i<m_Windows.Count; ++i)
		{
			if(m_Windows[i].m_Id==Node.m_Id)
				continue;
			if(m_Windows[i].m_OnStateAction==Node.m_OnStateAction && !m_Windows[i].m_AlreadyExists)
				return true;
		}
		return false;
	}
	//public bool DoesTransitionAlreadyExist(TransitionNode Node)
	//{
	//	StateNode l_EnterNode=GetStateNodeWithIndex(Node.m_EnterNode);
	//	if(l_EnterNode==null)
	//		return false;
	//	for(int i=0; i<l_EnterNode.m_Transitions.Count; ++i)
	//	{
	//		Transition l_Transition=l_EnterNode.m_Transitions[i]; 
	//		if(l_Transition.m_Condition==Node.m_PreviousCondition && Node.m_TransitionId!=l_Transition.m_Id)
	//			return true;
	//	}
	//	return false;
	//}
	public void SetEntryNode(int Id) 
	{
		if(GetStateNodeWithIndex(m_EntryNodeId)!=null)
			GetStateNodeWithIndex(m_EntryNodeId).m_IsEntryNode=false; 
		m_EntryNodeId=Id;
		GetStateNodeWithIndex(m_EntryNodeId).m_IsEntryNode=true;
	}
	public StateNode GetEntryNode() 
	{
		return GetStateNodeWithIndex(m_EntryNodeId);
	}
}



