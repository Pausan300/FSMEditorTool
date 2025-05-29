using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Editor/Nodes/Transition Node")]
public class TransitionNode : DrawNode
{
	public override void DrawWindow(Node Node)
	{
		Node l_EnterNode=FSMEditor.m_Settings.m_CurrentGraph.GetNodeWithIndex(Node.m_EnterNode);
		if(l_EnterNode==null)
			return;
		//if(l_EnterNode.m_StateReferences.m_Action==null)
		//{
		//	FSMEditor.m_Settings.m_CurrentGraph.DeleteNode(Node.m_Id);
		//	l_EnterNode.m_StateReferences.RemoveTransition(Node.m_TransitionReferences.m_TransitionId);
		//	return;
		//}

		//Transition l_Transition=l_EnterNode.m_StateReferences.m_CurrentState.GetTransition(Node.m_TransitionReferences.m_TransitionId);
		Transition l_Transition=l_EnterNode.m_StateReferences.GetTransition(Node.m_TransitionReferences.m_TransitionId);
		if(l_Transition==null)
			return;
		

		if(l_Transition.m_Condition==null)
		{
			EditorGUILayout.LabelField("No Condition assigned");
			Node.m_Assigned=false;
		}
		else
		{
			Node.m_Assigned=true;

			if(Node.m_AlreadyExists)
				EditorGUILayout.LabelField("Condition already exists");
			else
			{ 
				EditorGUILayout.LabelField("Condition");
				Node l_TargetNode=FSMEditor.m_Settings.m_CurrentGraph.GetNodeWithIndex(Node.m_TargetNode);
				if(l_TargetNode!=null)
				{
					if(!l_TargetNode.m_AlreadyExists)
						//l_Transition.m_TargetState=l_TargetNode.m_StateReferences.m_CurrentState;
						l_Transition.m_TargetState=l_TargetNode.m_Id;
					else
						//l_Transition.m_TargetState=null;
						l_Transition.m_TargetState=-1;
				}
				else
					//l_Transition.m_TargetState=null;
					l_Transition.m_TargetState=-1;
			}
		}

		l_Transition.m_Condition=(Condition)EditorGUILayout.ObjectField(l_Transition.m_Condition, typeof(Condition), false);
		Node.m_TransitionReferences.m_Condition=l_Transition.m_Condition;

		if(Node.m_TransitionReferences.m_PreviousCondition!=l_Transition.m_Condition)
		{
			Node.m_TransitionReferences.m_PreviousCondition=l_Transition.m_Condition;

			Node.m_AlreadyExists=FSMEditor.m_Settings.m_CurrentGraph.DoesTransitionAlreadyExist(Node);
		}
	}

	public override void DrawLine(Node Node)
	{
		Rect l_Rect=Node.m_WindowRect;
		l_Rect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
		l_Rect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;
		//l_Rect.width=1;
		//l_Rect.height=1;

		Node l_EnterNode=FSMEditor.m_Settings.m_CurrentGraph.GetNodeWithIndex(Node.m_EnterNode);
		if(l_EnterNode==null)
			FSMEditor.m_Settings.m_CurrentGraph.DeleteNode(Node.m_Id);
		else
		{
			Color l_TargetColor=Color.green;
			if(!Node.m_Assigned || Node.m_AlreadyExists || l_EnterNode.m_StateReferences.m_OnStateAction==null)
				l_TargetColor=Color.red;

			Rect l_PreviousRect=l_EnterNode.m_WindowRect;
			l_PreviousRect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
			l_PreviousRect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;

			FSMEditor.DrawNodeLine(l_PreviousRect, l_Rect, true, l_TargetColor);
		}
		
		if(Node.m_AlreadyExists)
			return;


			Node l_TargetNode=FSMEditor.m_Settings.m_CurrentGraph.GetNodeWithIndex(Node.m_TargetNode);
			if(l_TargetNode==null)
				Node.m_TargetNode=-1;
			else
			{
				Transition l_Transition=l_EnterNode.m_StateReferences.GetTransition(Node.m_TransitionReferences.m_TransitionId);

				//l_Rect=Node.m_WindowRect;
				//l_Rect.x+=l_Rect.width;
				Rect l_NextRect=l_TargetNode.m_WindowRect;
				l_NextRect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
				l_NextRect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;
				//l_NextRect.x-=l_NextRect.width*0.5f;

				Color l_TargetColor=Color.green;
				if(l_TargetNode.m_DrawNode is StateNode)
				{
					if(!l_TargetNode.m_Assigned || l_TargetNode.m_AlreadyExists || l_Transition.m_Condition==null)
						l_TargetColor=Color.red;
				}
				else
				{
					if(!l_TargetNode.m_Assigned)
						l_TargetColor=Color.red;
					else
						l_TargetColor=Color.cyan;
				}

				FSMEditor.DrawNodeLine(l_Rect, l_NextRect, false, l_TargetColor);
			}
		}
	

    public override void DrawLabel(Node Node)
    {
        GUI.Box(new Rect(Node.m_WindowRect.x, Node.m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, Node.m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight), "TRANSITION", FSMEditor.m_Settings.GetTransitionNodeLabelStyle());
    }
}
