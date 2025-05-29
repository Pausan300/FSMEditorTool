using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditorInternal.VR;

public class FSMEditor : EditorWindow
{
	public static EditorSettings m_Settings;

	Vector3 m_MousePos;
	bool m_ClickedOnWindow;
	bool m_MakeTransition;
	Node m_SelectedNode;
	int m_TransitionOrigin;
	Rect m_MouseRect=new Rect(0,0,1,1);
	Rect m_BgRect=new Rect(-5, -5, 10000, 10000);
	Vector2 m_ScrollPos;
	Vector2 m_ScrollStartPos;
	GUIStyle m_EditorStyle;

	public enum Inputs
	{ 
		ADDSTATE, 
		ADDTRANSITIONNODE,
		DELETENODE,
		CONNECTTRANSITION,
		ADDPOINTERNODE
	}

	[MenuItem("FSM Editor/Open Editor")]
	static void ShowEditor()
	{ 
		FSMEditor l_Editor=EditorWindow.GetWindow<FSMEditor>();
		l_Editor.minSize=new Vector2(990, 540);
		l_Editor.maxSize=new Vector2(1920, 1080);
	}

	private void OnEnable()
	{
		m_Settings=Resources.Load("EditorSettings") as EditorSettings;
		m_EditorStyle=m_Settings.m_EditorSkin.GetStyle("window");
	}
	private void OnGUI()
	{
		Event l_Event=Event.current;
		m_MousePos=l_Event.mousePosition;
		UserInput(l_Event);

		DrawNodes();
		GUI.Box(new Rect(0, 0, 225, 200), "", m_Settings.m_EditorSkin.GetStyle("Box"));
		EditorGUILayout.LabelField(" ", GUILayout.Width(100)); 
		EditorGUILayout.LabelField("Assign Graph:", GUILayout.Width(100));
		m_Settings.m_CurrentGraph=(Graph)EditorGUILayout.ObjectField(m_Settings.m_CurrentGraph, typeof(Graph), false, GUILayout.Width(200));
		if(GUI.Button(new Rect(5, 100, 100, 40), "Reset View"))
			ResetScroll();
		if(m_SelectedNode!=null) 
		{
			DrawSelectedNodeInspector();
		}

		if(l_Event.type==EventType.MouseDrag)
		{
			if(m_Settings.m_CurrentGraph!=null)
			{
				//m_Settings.m_CurrentGraph.DeleteWindows();
				Repaint();
			}
		}

		if(GUI.changed)
		{
			//m_Settings.m_CurrentGraph.DeleteWindows();
			Repaint();
		}

		if(m_MakeTransition)
		{
			m_MouseRect.x=m_MousePos.x;
			m_MouseRect.y=m_MousePos.y;
			Rect l_PreviousRect=m_Settings.m_CurrentGraph.GetNodeWithIndex(m_TransitionOrigin).m_WindowRect;
			l_PreviousRect.y-=m_Settings.m_NodeLabelHeight;
			l_PreviousRect.height+=m_Settings.m_NodeLabelHeight;
			DrawNodeLine(l_PreviousRect, m_MouseRect, true, Color.red);
			Repaint();
		}
	}
	void DrawNodes()
	{
		GUILayout.BeginArea(m_BgRect, m_EditorStyle);
		BeginWindows();

		if(m_Settings.m_CurrentGraph!=null)
		{
			foreach(Node node in m_Settings.m_CurrentGraph.m_Windows)		
				node.DrawLine();

			GUIStyle l_NodeWindowStyle = m_Settings.m_EditorSkin.GetStyle("LeftBox");

			int l_WindowIndex=0;
			int l_PointerId=0;
			for(int i=0; i<m_Settings.m_CurrentGraph.m_Windows.Count; ++i)
			{
				m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect=GUI.Window(l_WindowIndex, m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect, DrawNodeWindow, "");
				m_Settings.m_CurrentGraph.m_Windows[i].DrawLabel();
				l_WindowIndex++;
				//GUI.Box(new Rect(m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.x, m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.y-m_Settings.m_NodeLabelHeight, m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.width, m_Settings.m_NodeLabelHeight), "");
			}

			for(int i=0; i<m_Settings.m_CurrentGraph.m_Pointers.Count; ++i)
			{
				PointerNode l_Node=m_Settings.m_CurrentGraph.m_Pointers[i];
				l_Node.m_WindowRect=GUI.Window(l_WindowIndex, l_Node.m_WindowRect, id => DrawPointerWindow(id, l_Node), "");
				m_Settings.m_CurrentGraph.m_Pointers[i].DrawLabel();
				l_WindowIndex++;
				l_PointerId++;
				//GUI.Box(new Rect(m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.x, m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.y-m_Settings.m_NodeLabelHeight, m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.width, m_Settings.m_NodeLabelHeight), "");
			}
		} 

		EndWindows();
		GUILayout.EndArea();
	}
	void DrawNodeWindow(int Id)
	{
		m_Settings.m_CurrentGraph.m_Windows[Id].DrawWindow();
		GUI.DragWindow();
	}
	void DrawPointerWindow(int Id, PointerNode Pointer) 
	{
		Debug.Log(Pointer);
		Pointer.DrawWindoww();
		GUI.DragWindow();
	}
	void DrawSelectedNodeInspector()
    {
		Rect l_InspectorContainer=new Rect(0, 220, 225, 200);
		GUI.Box(l_InspectorContainer, "", m_Settings.m_EditorSkin.GetStyle("Box"));
		
		Rect l_PropertiesContainer=l_InspectorContainer;
		l_PropertiesContainer.x+=10.0f;
		l_PropertiesContainer.y+=20.0f;
		l_PropertiesContainer.width-=20.0f;
		GUILayout.BeginArea(l_PropertiesContainer);
		GUILayout.Space(10);
		bool l_HasProperties=false;
		SerializedObject l_Object=null;
		if(m_SelectedNode.m_TransitionReferences.m_Condition!=null) 
			l_Object=new SerializedObject(m_SelectedNode.m_TransitionReferences.m_Condition);
		else if(m_SelectedNode.m_StateReferences.m_OnStateAction!=null)
			l_Object=new SerializedObject(m_SelectedNode.m_StateReferences.m_OnStateAction);
		if(l_Object!=null) 
		{ 
			SerializedProperty l_ObjectProperties=l_Object.GetIterator();
			while(l_ObjectProperties.NextVisible(true)) 
			{
				if(l_ObjectProperties.name=="m_Script")
					continue;
				EditorGUILayout.PropertyField(l_ObjectProperties, true);
				l_HasProperties=true;
			}
			l_Object.ApplyModifiedProperties();
		}
		GUILayout.EndArea();

		if(l_HasProperties)
			GUI.Label(new Rect(l_InspectorContainer.x+10.0f, l_InspectorContainer.y+10.0f, l_InspectorContainer.width, 20.0f), "State Properties:", EditorStyles.boldLabel);
		else
			GUI.Label(new Rect(l_InspectorContainer.x+10.0f, l_InspectorContainer.y+10.0f, l_InspectorContainer.width, 20.0f), "No properties found", EditorStyles.boldLabel);
	}
	void UserInput(Event Event)
	{
		if(m_Settings.m_CurrentGraph==null)
			return;

		if(Event.button==1 && !m_MakeTransition)
		{
			if(Event.type==EventType.MouseDown)
				RightClick(Event);
		}
		if(Event.button==0 && Event.type==EventType.MouseDown)
		{
			if(m_MakeTransition) 
			{
				MakeTransition();
			}
			else 
			{
				TrySelectNode();
			}
		}
		if(Event.button==2)
		{ 
			if(Event.type==EventType.MouseDown)
				m_ScrollStartPos=Event.mousePosition;
			else if(Event.type==EventType.MouseDrag)
				MoveWindows(Event);
		}
	}
	void MoveWindows(Event Event)
	{
		Vector2 l_Distance=Event.mousePosition-m_ScrollStartPos;
		l_Distance*=0.6f;
		m_ScrollStartPos=Event.mousePosition;
		m_ScrollPos+=l_Distance;

		for(int i=0; i<m_Settings.m_CurrentGraph.m_Windows.Count; ++i)
		{
			Node l_Node=m_Settings.m_CurrentGraph.m_Windows[i];
			l_Node.m_WindowRect.x+=l_Distance.x;
			l_Node.m_WindowRect.y+=l_Distance.y;
		}
	}
	void ResetScroll()
	{
		for(int i=0; i<m_Settings.m_CurrentGraph.m_Windows.Count; ++i)
		{
			Node l_Node=m_Settings.m_CurrentGraph.m_Windows[i];
			l_Node.m_WindowRect.x-=m_ScrollPos.x;
			l_Node.m_WindowRect.y-=m_ScrollPos.y;
		}
		m_ScrollPos=Vector2.zero;
	}
	void RightClick(Event Event)
	{
		m_ClickedOnWindow=false;

		TrySelectNode();

		if(!m_ClickedOnWindow)
			AddNewNode(Event);
		else
			ModifyNode(Event);
	}
	void TrySelectNode() 
	{
		for(int i=0; i<m_Settings.m_CurrentGraph.m_Windows.Count; ++i)
		{
			if(m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.Contains(m_MousePos))
			{
				m_ClickedOnWindow=true;
				m_SelectedNode=m_Settings.m_CurrentGraph.m_Windows[i];
				break;
			}
		}
	}
	void MakeTransition()
	{
		m_MakeTransition=false;
		m_ClickedOnWindow=false;
		for(int i=0; i<m_Settings.m_CurrentGraph.m_Windows.Count; ++i)
		{
			if(m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.Contains(m_MousePos))
			{
				m_ClickedOnWindow=true;
				m_SelectedNode=m_Settings.m_CurrentGraph.m_Windows[i];
				break;
			}
		}

		if(m_ClickedOnWindow)
		{
			if(m_SelectedNode.m_DrawNode is StateNode || m_SelectedNode.m_DrawNode is PointerNode)
			{
				if(m_SelectedNode.m_Id!=m_TransitionOrigin)
				{ 
					Node l_TransitionNode=m_Settings.m_CurrentGraph.GetNodeWithIndex(m_TransitionOrigin);
					l_TransitionNode.m_TargetNode=m_SelectedNode.m_Id;

					Node l_EnterNode=m_Settings.m_CurrentGraph.GetNodeWithIndex(l_TransitionNode.m_EnterNode);
					//Transition l_Transition=l_EnterNode.m_StateReferences.m_CurrentState.GetTransition(l_TransitionNode.m_TransitionReferences.m_TransitionId);
					Transition l_Transition=l_EnterNode.m_StateReferences.GetTransition(l_TransitionNode.m_TransitionReferences.m_TransitionId);
					//l_Transition.m_TargetState=m_SelectedNode.m_StateReferences.m_CurrentState;
					l_Transition.m_TargetState=m_SelectedNode.m_Id;
				}
			}
		}
	}
	void AddNewNode(Event Event)
	{
		GenericMenu l_Menu=new GenericMenu();
		l_Menu.AddSeparator("");
		if(m_Settings.m_CurrentGraph!=null)
		{
			//l_Menu.AddItem(new GUIContent("Add State"), false, ContextCallback, Inputs.ADDSTATE); 
			l_Menu.AddItem(new GUIContent("Add State"), false, AddStateNode); 
			//l_Menu.AddItem(new GUIContent("Add Pointer"), false, ContextCallback, Inputs.ADDPOINTERNODE); 
			l_Menu.AddItem(new GUIContent("Add Pointer"), false, AddPointerNode); 
		} 
		else
		{
			l_Menu.AddDisabledItem(new GUIContent("Add State")); 
		}
		l_Menu.ShowAsContext();
		Event.Use();
	}
	void ModifyNode(Event Event)
	{
		GenericMenu l_Menu = new GenericMenu();
		if(m_SelectedNode.m_DrawNode is StateNode)
		{
			if(m_SelectedNode.m_StateReferences.m_OnStateAction!=null && !m_SelectedNode.m_AlreadyExists)
			{
				l_Menu.AddSeparator("");
				l_Menu.AddItem(new GUIContent("Add Transition"), false, AddTransitionNode);
				if(!m_SelectedNode.m_StateReferences.m_IsEntryNode) 
				{
					l_Menu.AddSeparator("");
					l_Menu.AddItem(new GUIContent("Set as Entry State"), false, SetEntryNode);
				}
			} 
			//else
			//{
			//	l_Menu.AddSeparator("");
			//	l_Menu.AddDisabledItem(new GUIContent("Add Transition"));
			//}
			l_Menu.AddSeparator("");
			//l_Menu.AddItem(new GUIContent("Delete"),false,ContextCallback,Inputs.DELETENODE);
			l_Menu.AddItem(new GUIContent("Delete"), false, RemoveNode);
		} 
		else if(m_SelectedNode.m_DrawNode is PointerNode)
		{
			l_Menu.AddSeparator("");
			//l_Menu.AddItem(new GUIContent("Delete"),false,ContextCallback,Inputs.DELETENODE);
			l_Menu.AddItem(new GUIContent("Delete"), false, RemoveNode);
		} 
		else if(m_SelectedNode.m_DrawNode is TransitionNode)
		{
			if(m_SelectedNode.m_AlreadyExists || !m_SelectedNode.m_Assigned)
			{ 
				l_Menu.AddSeparator("");
				l_Menu.AddDisabledItem(new GUIContent("Connect Transition"));				
			}
			else
			{
				l_Menu.AddSeparator("");
				//l_Menu.AddItem(new GUIContent("Connect Transition"),false,ContextCallback,Inputs.CONNECTTRANSITION);
				l_Menu.AddItem(new GUIContent("Connect Transition"), false, SetMakeTransition);
			}
			l_Menu.AddSeparator("");
			//l_Menu.AddItem(new GUIContent("Delete"),false,ContextCallback,Inputs.DELETENODE);
			l_Menu.AddItem(new GUIContent("Delete"), false, RemoveNode);
		}
		l_Menu.ShowAsContext();
		Event.Use();
	}
	//void ContextCallback(object Object)
	//{
	//	Inputs l_Action = (Inputs)Object;
	//	switch(l_Action)
	//	{
	//		case Inputs.ADDSTATE:
	//			m_Settings.AddNodeOnGraph(m_Settings.m_StateNode, 200, 100, "State", m_MousePos);
	//			//m_Settings.AddStateNodeOnGraph(200, 100, "State", m_MousePos);
	//			break;
	//		case Inputs.ADDPOINTERNODE:
	//			m_Settings.AddNodeOnGraph(m_Settings.m_PointerNode, 150, 70, "Pointer", m_MousePos);
	//			//m_Settings.AddPointerNodeOnGraph(150, 70, "Pointer", m_MousePos);
	//			break;
	//		case Inputs.ADDTRANSITIONNODE:
	//			//AddTransitionNode(m_SelectedNode, m_MousePos);
	//			AddTransitionNode();
	//			break;
	//		case Inputs.DELETENODE:
	//			if(m_SelectedNode.m_DrawNode is TransitionNode)
	//			{
	//				Node l_EnterNode=m_Settings.m_CurrentGraph.GetNodeWithIndex(m_SelectedNode.m_EnterNode);
	//				//l_EnterNode.m_StateReferences.m_CurrentState.RemoveTransition(m_SelectedNode.m_TransitionReferences.m_TransitionId);
	//				l_EnterNode.m_StateReferences.RemoveTransition(m_SelectedNode.m_TransitionReferences.m_TransitionId);
	//			}
	//			m_Settings.m_CurrentGraph.DeleteNode(m_SelectedNode.m_Id);
	//			//m_Settings.m_CurrentGraph.DeleteWindows();
	//			break;
	//		case Inputs.CONNECTTRANSITION:
	//			m_TransitionOrigin=m_SelectedNode.m_Id;
	//			m_MakeTransition=true;
	//			break;
	//	}
	//	EditorUtility.SetDirty(m_Settings);
	//}
	void RemoveNode() 
	{
		if(m_SelectedNode.m_DrawNode is TransitionNode)
		{
			Node l_EnterNode=m_Settings.m_CurrentGraph.GetNodeWithIndex(m_SelectedNode.m_EnterNode);
			//l_EnterNode.m_StateReferences.m_CurrentState.RemoveTransition(m_SelectedNode.m_TransitionReferences.m_TransitionId);
			l_EnterNode.m_StateReferences.RemoveTransition(m_SelectedNode.m_TransitionReferences.m_TransitionId);
		}
		m_Settings.m_CurrentGraph.DeleteNode(m_SelectedNode.m_Id);
		m_Settings.Save();
	}
	void SetMakeTransition() 
	{
		m_TransitionOrigin=m_SelectedNode.m_Id;
		m_MakeTransition=true;
	}

	//Node AddTransitionNode(Node EnterNode, Vector3 Pos)
	void AddTransitionNode()
	{
		Node l_TransitionNode = AddNodeOnGraph(m_Settings.m_TransitionNode, 200, 100, "Condition", m_MousePos);
		//Node l_TransitionNode=m_Settings.AddTransitionNodeOnGraph(200, 100, "Condition", m_MousePos);
		//l_TransitionNode.m_EnterNode=EnterNode.m_Id;
		l_TransitionNode.m_EnterNode=m_SelectedNode.m_Id;
		//Transition l_Transition=EnterNode.m_StateReferences.AddTransition();
		Transition l_Transition=m_SelectedNode.m_StateReferences.AddTransition();
		//l_TransitionNode.m_TransitionReferences.m_TransitionId=EnterNode.m_StateReferences.m_Transitions.Count-1;
		l_TransitionNode.m_TransitionReferences.m_TransitionId=m_SelectedNode.m_StateReferences.m_Transitions.Count-1;
		//Transition l_Transition=m_Settings.m_StateNode.AddTransition(EnterNode);
		//l_Transition.m_Id=EnterNode.m_StateReferences.m_Transitions.Count-1;
		l_Transition.m_Id=m_SelectedNode.m_StateReferences.m_Transitions.Count-1;
		//l_TransitionNode.m_TransitionReferences.m_TransitionId=l_Transition.m_Id;
		//return l_TransitionNode;
		m_Settings.Save();
	}
	void AddStateNode() 
	{
		Node l_StateNode=AddNodeOnGraph(m_Settings.m_StateNode, 200, 210, "State", m_MousePos);
		l_StateNode.m_TransitionReferences.m_TransitionId=-1;
		m_Settings.Save();
	}
	void AddPointerNode() 
	{
		//AddNodeOnGraph(m_Settings.m_PointerNode, 150, 70, "Pointer", m_MousePos); 
		AddPointerNodeOnGraph(150, 70, "Pointer", m_MousePos); 
		m_Settings.Save();
	}
	Node AddNodeOnGraph(DrawNode Type, float Width, float Height, string Title, Vector3 Pos)
	{
		Node l_Node=new Node();
		l_Node.m_DrawNode=Type;
		l_Node.m_WindowRect.width=Width;
		l_Node.m_WindowRect.height=Height;
		l_Node.m_WindowTitle=Title;
		l_Node.m_WindowRect.x=Pos.x;
		l_Node.m_WindowRect.y=Pos.y;
		//l_Node.m_Id=m_CurrentGraph.m_Windows.Count;
		l_Node.m_Id=m_Settings.m_CurrentGraph.m_Id; 
		l_Node.m_TransitionReferences=new TransitionNodeReferences();
		l_Node.m_StateReferences=new StateNodeReferences();
		m_Settings.m_CurrentGraph.m_Id++;
		if(m_Settings.m_CurrentGraph.m_Windows.Count<=0) 
		{
			m_SelectedNode=l_Node;
			m_Settings.m_CurrentGraph.m_Windows.Add(l_Node); 
			SetEntryNode();
		}
		else
			m_Settings.m_CurrentGraph.m_Windows.Add(l_Node);
		return l_Node;
	}
	PointerNode AddPointerNodeOnGraph(float Width, float Height, string Title, Vector3 Pos)
	{
		PointerNode l_Node=new PointerNode();
		//l_Node.m_DrawNode=Type;
		l_Node.m_WindowRect.width=Width;
		l_Node.m_WindowRect.height=Height;
		l_Node.m_WindowTitle=Title;
		l_Node.m_WindowRect.x=Pos.x;
		l_Node.m_WindowRect.y=Pos.y;
		//l_Node.m_Id=m_CurrentGraph.m_Windows.Count;
		l_Node.m_Id=m_Settings.m_CurrentGraph.m_Id; 
		l_Node.m_TransitionReferences=new TransitionNodeReferences();
		l_Node.m_StateReferences=new StateNodeReferences();
		m_Settings.m_CurrentGraph.m_Id++;
		m_Settings.m_CurrentGraph.m_Pointers.Add(l_Node); 
		return l_Node;
	}
	void SetEntryNode() 
	{
		m_Settings.m_CurrentGraph.SetEntryNode(m_SelectedNode.m_Id);
		m_Settings.Save();
	}
	//public static Node AddTransitionNodeFromTransition(Transition Transition, Node EnterNode, Vector3 Pos)
	//{
	//	Node l_TransitionNode=m_Settings.AddNodeOnGraph(m_Settings.m_TransitionNode, 200, 100, "Condition", Pos);
	//	l_TransitionNode.m_EnterNode=EnterNode.m_Id;
	//	l_TransitionNode.m_TransitionReferences.m_TransitionId=Transition.m_Id;
	//	return l_TransitionNode;
	//}

	public static void DrawNodeLine(Rect Start, Rect End, bool Left, Color Color)
	{
		//Vector3 l_StartPos=new Vector3((Left) ? Start.x+Start.width : Start.x, Start.y+(Start.height*0.5f), 0);
		//Vector3 l_EndPos=new Vector3(End.x+(End.width*0.5f), End.y+(End.height*0.5f), 0);
		Vector3 l_StartPos=FindRectClosestPos(Start, new Vector2(End.x, End.y));
        Vector3 l_EndPos=FindRectClosestPos(End, new Vector2(Start.x, Start.y));

		Handles.color=Color;
		//Handles.DrawLine(l_StartPos, l_EndPos);
		//Handles.DrawLine(l_StartPos, (l_StartPos+l_EndPos)*0.5f);
		Handles.DrawDottedLine(l_StartPos, (l_StartPos+l_EndPos)*0.5f, 1.0f);
		Handles.DrawAAPolyLine(5, (l_StartPos+l_EndPos)*0.5f, l_EndPos);
		//Handles.DrawLine((l_StartPos+l_EndPos)*0.5f, l_EndPos, 1.0f);
		//Handles.DrawDottedLine((l_StartPos+l_EndPos)*0.5f, l_EndPos, 1.0f);

		float l_ArrowLength=20.0f;
        float l_ArrowAngle=135f;
        Vector3 l_LineDir=(l_EndPos-l_StartPos).normalized;
        Vector3 l_RightDir=Quaternion.Euler(0, 0, l_ArrowAngle)*l_LineDir;
        Vector3 l_LeftDir=Quaternion.Euler(0, 0, -l_ArrowAngle)*l_LineDir;
        Handles.DrawAAPolyLine(5, l_EndPos, l_EndPos-l_LineDir*l_ArrowLength+l_RightDir*l_ArrowLength);
        Handles.DrawAAPolyLine(5, l_EndPos, l_EndPos-l_LineDir*l_ArrowLength+l_LeftDir*l_ArrowLength);
	}
    public static Vector2 FindRectClosestPos(Rect _Rect, Vector2 Target)
    {
        float l_PosX=Mathf.Clamp(Target.x, _Rect.xMin, _Rect.xMax);
        float l_PosY=Mathf.Clamp(Target.y, _Rect.yMin, _Rect.yMax);
        return new Vector2(l_PosX, l_PosY);
    }
}
