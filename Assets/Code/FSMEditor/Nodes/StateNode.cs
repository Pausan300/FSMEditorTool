using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.IO;

[CreateAssetMenu(menuName = "Editor/Nodes/State Node")]
public class StateNode : DrawNode
{
	public override void DrawWindow(Node Node)
	{
		//if(Node.m_StateReferences.m_CurrentState==null)
		//	EditorGUILayout.LabelField("Add state to modify:");
		//else
		//{
		//	if(Node.m_Collapse)
		//		Node.m_WindowRect.height=100;
		//}

		//if(Node.m_StateReferences.m_CurrentState!=null)
		//{
		//	if(Node.m_StateReferences.m_CurrentState.m_CurrentlyExecuting && Application.isPlaying)
		//	{
		//		EditorSettings m_Settings = Resources.Load("EditorSettings") as EditorSettings;
		//		GUIStyle m_EditorStyle = m_Settings.m_EditorSkin.GetStyle("label");
		//		EditorGUILayout.LabelField("CURRENTLY EXECUTING",m_EditorStyle);
		//	}
		//	else if(!Application.isPlaying && Node.m_StateReferences.m_CurrentState.m_CurrentlyExecuting)
		//		Node.m_StateReferences.m_CurrentState.m_CurrentlyExecuting=false;
		//}

		//Node.m_StateReferences.m_CurrentState=(State)EditorGUILayout.ObjectField(Node.m_StateReferences.m_CurrentState, typeof(State), false);

		//if(Node.m_WasCollapse!=Node.m_Collapse)
		//	Node.m_WasCollapse=Node.m_Collapse;


		//if(Node.m_StateReferences.m_PreviousState!=Node.m_StateReferences.m_CurrentState)
		//{
		//	Node.m_AlreadyExists=FSMEditor.m_Settings.m_CurrentGraph.DoesNodeAlreadyExist(Node);
		//	Node.m_StateReferences.m_PreviousState=Node.m_StateReferences.m_CurrentState;
		//	if(!Node.m_AlreadyExists)
		//	{
		//		Vector3 l_Position=new Vector3(Node.m_WindowRect.x, Node.m_WindowRect.y, 0);
		//		l_Position.x+=Node.m_WindowRect.width*2;

		//		//SetUpReordableLists(Node);

		//		for(int i=0; i<Node.m_StateReferences.m_CurrentState.m_Transitions.Count; ++i)
		//		{
		//			l_Position.y+=i*100;
		//			FSMEditor.AddTransitionNodeFromTransition(Node.m_StateReferences.m_CurrentState.m_Transitions[i], Node, l_Position);
		//		}
		//	}
		//}
		if(Node.m_StateReferences.m_OnEnterAction==null)
		{
			EditorGUILayout.LabelField("No OnEnter assigned");
		}
		else 
		{
			EditorGUILayout.LabelField("OnEnter Action");
		}
		Node.m_StateReferences.m_OnEnterAction=(StateAction)EditorGUILayout.ObjectField(Node.m_StateReferences.m_OnEnterAction, typeof(StateAction), false);
		EditorGUILayout.LabelField("");

		//if(Node.m_AlreadyExists)
		//{
		//	EditorGUILayout.LabelField("State already exists");
		//	Node.m_WindowRect.height=100;
		//	return;
		//}

		if(Node.m_StateReferences.m_OnStateAction==null)
		{
			EditorGUILayout.LabelField("ONSTATE ACTION NEEDED", FSMEditor.m_Settings.GetRedTextStyle());
		}
		else 
		{
			EditorGUILayout.LabelField("OnState Action");
		}
		Node.m_StateReferences.m_OnStateAction=(StateAction)EditorGUILayout.ObjectField(Node.m_StateReferences.m_OnStateAction, typeof(StateAction), false);
		EditorGUILayout.LabelField("");	
		
		if(Node.m_StateReferences.m_OnExitAction==null)
		{
			EditorGUILayout.LabelField("No OnExit assigned");
		}
		else 
		{
			EditorGUILayout.LabelField("OnExit Action");
		}
		Node.m_StateReferences.m_OnExitAction=(StateAction)EditorGUILayout.ObjectField(Node.m_StateReferences.m_OnExitAction, typeof(StateAction), false);
		EditorGUILayout.LabelField("");

		//if(Node.m_StateReferences.m_CurrentState!=null)
		if(Node.m_StateReferences.m_OnStateAction!=null)
		{
			Node.m_Assigned=true;

			//if(!Node.m_Collapse)
			//{

				//if(Node.m_StateReferences.m_SerializedState==null)
				//	SetUpReordableLists(Node);

				//Node.m_StateReferences.m_SerializedState.Update();
				//EditorGUILayout.LabelField("");
				//Node.m_StateReferences.m_OnStateList.DoLayoutList();

				//Node.m_StateReferences.m_SerializedState.ApplyModifiedProperties();

				//float l_StandardHeight=300;
				//l_StandardHeight+=Node.m_StateReferences.m_OnStateList.count*20;
				//Node.m_WindowRect.height=l_StandardHeight;
			//}
		}
		else
			Node.m_Assigned=false;
	}


	//void SetUpReordableLists(Node Node)
	//{
	//	Node.m_StateReferences.m_SerializedState=new SerializedObject(Node.m_StateReferences.m_CurrentState);

	//	Node.m_StateReferences.m_OnStateList=new ReorderableList(Node.m_StateReferences.m_SerializedState, Node.m_StateReferences.m_SerializedState.FindProperty("m_OnState"), true, true, true, true);

	//	HandleReordableList(Node.m_StateReferences.m_OnStateList, "On State Action");
	//}
	//void HandleReordableList(ReorderableList List, string TargetName)
	//{
	//	List.drawHeaderCallback=(Rect l_Rect) =>
	//	{ 
	//		EditorGUI.LabelField(l_Rect, TargetName);
	//	};
	//	List.drawElementCallback=(Rect l_Rect, int Index, bool IsActive, bool IsFocused) =>
	//	{
	//		var l_Element=List.serializedProperty.GetArrayElementAtIndex(Index);
	//		EditorGUI.ObjectField(new Rect(l_Rect.x, l_Rect.y, l_Rect.width, EditorGUIUtility.singleLineHeight), l_Element, GUIContent.none);
	//	};
	//}
	public override void DrawLine(Node Node)
	{
		
	}

	public override void DrawLabel(Node Node)
    {
		GUI.Box(new Rect(Node.m_WindowRect.x, Node.m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, Node.m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight), "STATE", FSMEditor.m_Settings.GetStateNodeLabelStyle());
		if(Node.m_StateReferences.m_IsEntryNode)
			EditorGUI.LabelField(new Rect(Node.m_WindowRect.x+Node.m_WindowRect.width/3.0f, Node.m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, Node.m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight), "(ENTRY STATE)");
			//GUI.Box(new Rect(Node.m_WindowRect.x, Node.m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, Node.m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight), "STATE", FSMEditor.m_Settings.GetStateNodeLabelStyle());
	}
    //public Transition AddTransition(Node Node)
    //{
    //	return Node.m_StateReferences.m_CurrentState.AddTransition();
    //}
}
