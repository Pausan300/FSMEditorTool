using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class FSMEditor : EditorWindow
{
	public static EditorSettings m_Settings;
	GUIStyle m_EditorStyle;

	Vector3 m_MousePos;
	Rect m_MouseRect=new Rect(0,0,1,1);
	Vector2 m_ScrollPos;
	Vector2 m_ScrollStartPos;
	bool m_ClickedOnWindow;
	bool m_MakeTransition;
	Node m_SelectedNode;
	int m_TransitionOrigin;

	[MenuItem("FSM Editor/Open Editor")]
	static void ShowEditor()
	{ 
		FSMEditor l_Editor=EditorWindow.GetWindow<FSMEditor>();
		l_Editor.minSize=m_Settings.m_MinEditorSize;
		l_Editor.maxSize=m_Settings.m_MaxEditorSize;
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
		DrawGraphConfig();
		if(m_ClickedOnWindow)
			DrawNodeInspector();

		if(l_Event.type==EventType.MouseDrag && m_Settings.m_CurrentGraph!=null)
			Repaint();

		if(GUI.changed)
			Repaint();

		if(m_MakeTransition)
		{
			m_MouseRect.x=m_MousePos.x;
			m_MouseRect.y=m_MousePos.y;
			Rect l_PreviousRect=m_Settings.m_CurrentGraph.GetNodeWithIndex(m_TransitionOrigin).m_WindowRect;
			l_PreviousRect.y-=m_Settings.m_NodeLabelHeight;
			l_PreviousRect.height+=m_Settings.m_NodeLabelHeight;
			DrawNodeLine(l_PreviousRect, m_MouseRect, Color.red);
			Repaint();
		}
	}

	void DrawNodes()
	{
		GUILayout.BeginArea(m_Settings.m_EditorWindowRect, m_EditorStyle);
		BeginWindows();

		if(m_Settings.m_CurrentGraph!=null)
		{
			GUIStyle l_NodeWindowStyle=new GUIStyle(GUI.skin.window);
			l_NodeWindowStyle.padding.top-=20;

			int l_WindowIndex=0;
			for(int i=0; i<m_Settings.m_CurrentGraph.m_Windows.Count; ++i)
			{
				StateNode l_Node=m_Settings.m_CurrentGraph.m_Windows[i];
				Rect l_WindowRect=new Rect(l_Node.m_WindowRect.x, l_Node.m_WindowRect.y, m_Settings.m_StateNodeWidth, m_Settings.m_StateNodeHeight);
				l_Node.m_WindowRect=GUI.Window(l_WindowIndex, l_WindowRect, id => DrawStateNodeWindow(id, l_Node), "", l_NodeWindowStyle);
				m_Settings.m_CurrentGraph.m_Windows[i].DrawLabel();
				l_WindowIndex++;
			}

			for(int i=0; i<m_Settings.m_CurrentGraph.m_TransitionNodes.Count; ++i)
			{
				TransitionNode l_Node=m_Settings.m_CurrentGraph.m_TransitionNodes[i];
				Rect l_WindowRect=new Rect(l_Node.m_WindowRect.x, l_Node.m_WindowRect.y, m_Settings.m_TransitionNodeWidth, m_Settings.m_TransitionNodeHeight);
				l_Node.m_WindowRect=GUI.Window(l_WindowIndex, l_WindowRect, id => DrawTransitionNodeWindow(id, l_Node), "", l_NodeWindowStyle);
				l_Node.DrawLabel();
				l_Node.DrawLine();
				l_WindowIndex++;
			}

			for(int i=0; i<m_Settings.m_CurrentGraph.m_PointerNodes.Count; ++i)
			{
				PointerNode l_Node=m_Settings.m_CurrentGraph.m_PointerNodes[i];
				l_Node.m_WindowRect=GUI.Window(l_WindowIndex, l_Node.m_WindowRect, id => DrawPointerNodeWindow(id, l_Node), "");
				l_Node.DrawLabel();
				l_WindowIndex++;
			}
		} 

		EndWindows();
		GUILayout.EndArea();
	}
	void DrawStateNodeWindow(int Id, StateNode Node)
	{
		Node.DrawWindow();
		GUI.DragWindow();
	}
	void DrawTransitionNodeWindow(int Id, TransitionNode Node) 
	{
		Node.DrawWindow();
		GUI.DragWindow();
	}
	void DrawPointerNodeWindow(int Id, PointerNode Node) 
	{
		Node.DrawWindow();
		GUI.DragWindow();
	}

	public static void DrawNodeLine(Rect Start, Rect End, Color Color)
	{
		Vector3 l_StartPos=FindRectClosestPos(Start, new Vector2(End.x, End.y));
        Vector3 l_EndPos=FindRectClosestPos(End, new Vector2(Start.x, Start.y));

		Handles.color=Color;
		Handles.DrawDottedLine(l_StartPos, (l_StartPos+l_EndPos)*0.5f, 1.0f);
		Handles.DrawAAPolyLine(5, (l_StartPos+l_EndPos)*0.5f, l_EndPos);

		float l_ArrowLength=20.0f;
        float l_ArrowAngle=135f;
        Vector3 l_LineDir=(l_EndPos-l_StartPos).normalized;
        Vector3 l_RightDir=Quaternion.Euler(0, 0, l_ArrowAngle)*l_LineDir;
        Vector3 l_LeftDir=Quaternion.Euler(0, 0, -l_ArrowAngle)*l_LineDir;
        Handles.DrawAAPolyLine(5, l_EndPos, l_EndPos-l_LineDir*l_ArrowLength+l_RightDir*l_ArrowLength);
        Handles.DrawAAPolyLine(5, l_EndPos, l_EndPos-l_LineDir*l_ArrowLength+l_LeftDir*l_ArrowLength);
	}
    public static Vector2 FindRectClosestPos(Rect Start, Vector2 End)
    {
        float l_PosX=Mathf.Clamp(End.x, Start.xMin, Start.xMax);
        float l_PosY=Mathf.Clamp(End.y, Start.yMin, Start.yMax);
        return new Vector2(l_PosX, l_PosY);
    }

	void DrawGraphConfig() 
	{
		GUI.Box(m_Settings.m_GraphConfigRect, "", m_Settings.m_EditorSkin.GetStyle("Box"));

		Rect l_ConfigContainer=m_Settings.m_GraphConfigRect;
		l_ConfigContainer.x+=15.0f;
		l_ConfigContainer.y+=20.0f;
		l_ConfigContainer.width-=30.0f; 

		GUILayout.BeginArea(l_ConfigContainer);
		GUILayout.Space(10.0f);

		EditorGUILayout.LabelField("ASSIGNED GRAPH", EditorStyles.whiteBoldLabel);
		m_Settings.m_CurrentGraph=(Graph)EditorGUILayout.ObjectField(m_Settings.m_CurrentGraph, typeof(Graph), false);
		GUILayout.Space(10.0f);

		if(GUILayout.Button("SAVE GRAPH"))
			m_Settings.Save();
		GUILayout.Space(20.0f);
		if(GUILayout.Button("Reset View"))
			ResetScroll();

		GUILayout.EndArea();
	}
	void ResetScroll()
	{
		for(int i=0; i<m_Settings.m_CurrentGraph.m_Windows.Count; ++i)
		{
			Node l_Node=m_Settings.m_CurrentGraph.m_Windows[i];
			l_Node.m_WindowRect.x-=m_ScrollPos.x;
			l_Node.m_WindowRect.y-=m_ScrollPos.y;
		}
		for(int i=0; i<m_Settings.m_CurrentGraph.m_TransitionNodes.Count; ++i)
		{
			Node l_Node=m_Settings.m_CurrentGraph.m_TransitionNodes[i];
			l_Node.m_WindowRect.x-=m_ScrollPos.x;
			l_Node.m_WindowRect.y-=m_ScrollPos.y;
		}
		m_ScrollPos=Vector2.zero;
	}

	void DrawNodeInspector()
    {
		GUI.Box(m_Settings.m_NodeInspectorRect, "", m_Settings.m_EditorSkin.GetStyle("Box"));
		
		Rect l_PropertiesContainer=m_Settings.m_NodeInspectorRect;
		l_PropertiesContainer.x+=15.0f;
		l_PropertiesContainer.y+=20.0f;
		l_PropertiesContainer.width-=30.0f; 
		GUILayout.BeginArea(l_PropertiesContainer);
		GUILayout.Space(10.0f);
		EditorGUILayout.LabelField("NODE INSPECTOR", m_Settings.GetCenteredTextStyle());
		GUILayout.Space(20.0f);

		bool l_HasProperties=false;
		if(m_SelectedNode!=null) 
		{
			SerializedObject l_Object=null;
			if(m_SelectedNode is TransitionNode _TransitionNode)
			{
				if(_TransitionNode.m_Condition!=null) 
				{
					l_Object=new SerializedObject(_TransitionNode.m_Condition);
					if(DrawNodeProperties(l_Object, "--- Condition Properties ---"))
						l_HasProperties=true;
				}
			}
			else if(m_SelectedNode is StateNode _StateNode)
			{
				EditorGUILayout.LabelField("State Name", EditorStyles.boldLabel);
				_StateNode.m_WindowTitle=EditorGUILayout.TextField(_StateNode.m_WindowTitle);
				GUILayout.Space(10.0f);
				if(_StateNode.m_OnEnterAction!=null) 
				{
					l_Object=new SerializedObject(_StateNode.m_OnEnterAction);
					if(DrawNodeProperties(l_Object, "--- OnEnter Properties ---"))
						l_HasProperties=true;
				}
				if(_StateNode.m_OnStateAction!=null) 
				{
					l_Object=new SerializedObject(_StateNode.m_OnStateAction);
					if(DrawNodeProperties(l_Object, "--- OnState Properties ---"))
						l_HasProperties=true;
				}
				if(_StateNode.m_OnExitAction!=null) 
				{
					l_Object=new SerializedObject(_StateNode.m_OnExitAction);
					if(DrawNodeProperties(l_Object, "--- OnExit Properties ---"))
						l_HasProperties=true;
				}
			}
		}
		if(!l_HasProperties)
			EditorGUILayout.LabelField("No Properties found", EditorStyles.boldLabel);

		GUILayout.EndArea();
	}
	bool DrawNodeProperties(SerializedObject Object, string Label) 
	{
		bool l_HasProperties=false;
		bool l_WrittenLabel=false;
		if(Object!=null)
        { 
			SerializedProperty l_ObjectProperties=Object.GetIterator();
			while(l_ObjectProperties.NextVisible(true)) 
			{
				if(l_ObjectProperties.name=="m_Script")
					continue;
				if(!l_WrittenLabel) 
				{
					EditorGUILayout.LabelField(Label, EditorStyles.boldLabel);
					l_WrittenLabel=true;
				}
				EditorGUILayout.PropertyField(l_ObjectProperties, true);
				l_HasProperties=true;
            }
			GUILayout.Space(10.0f);
			Object.ApplyModifiedProperties();
		}
		return l_HasProperties;
	}

	void UserInput(Event Event)
	{
		if(m_Settings.m_CurrentGraph==null)
			return;

		if(Event.button==0 && Event.type==EventType.MouseDown)
		{
			if(m_MakeTransition)
				MakeTransition();
			else
				TrySelectNode();
		}
		if(Event.button==1 && !m_MakeTransition)
		{
			if(Event.type==EventType.MouseDown)
				RightClick(Event);
		}
		if(Event.button==2)
		{ 
			if(Event.type==EventType.MouseDown)
				m_ScrollStartPos=Event.mousePosition;
			else if(Event.type==EventType.MouseDrag)
				MoveWindows(Event);
		}
	}
	void TrySelectNode() 
	{
		GUI.FocusControl(null);
		for(int i=0; i<m_Settings.m_CurrentGraph.m_Windows.Count; ++i)
		{
			if(m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.Contains(m_MousePos))
			{
				m_ClickedOnWindow=true;
				m_SelectedNode=m_Settings.m_CurrentGraph.m_Windows[i];
				return;
			}
		}
		for(int i=0; i<m_Settings.m_CurrentGraph.m_TransitionNodes.Count; ++i)
		{
			if(m_Settings.m_CurrentGraph.m_TransitionNodes[i].m_WindowRect.Contains(m_MousePos))
			{
				m_ClickedOnWindow=true;
				m_SelectedNode=m_Settings.m_CurrentGraph.m_TransitionNodes[i];
				return;
			}
		}
		if(m_Settings.m_NodeInspectorRect.Contains(m_MousePos)) 
		{
			m_ClickedOnWindow=true;
			return;
		}
		m_ClickedOnWindow=false;
	}
	void MakeTransition()
	{
		m_MakeTransition=false;
		m_ClickedOnWindow=false;
		TrySelectNode();

		if(m_ClickedOnWindow && (m_SelectedNode is StateNode || m_SelectedNode is PointerNode) && m_SelectedNode.m_Id!=m_TransitionOrigin)
		{
			TransitionNode l_TransitionNode=m_Settings.m_CurrentGraph.GetTransitionNodeWithIndex(m_TransitionOrigin);
			l_TransitionNode.m_TargetNodeId=m_SelectedNode.m_Id;

			//StateNode l_EnterNode=m_Settings.m_CurrentGraph.GetStateNodeWithIndex(l_TransitionNode.m_EnterNode);
			//Transition l_Transition=l_EnterNode.GetTransition(l_TransitionNode.m_TransitionId);
			//l_Transition.m_TargetState=m_SelectedNode.m_Id;
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
			m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.x+=l_Distance.x;
			m_Settings.m_CurrentGraph.m_Windows[i].m_WindowRect.y+=l_Distance.y;
		}
		for(int i=0; i<m_Settings.m_CurrentGraph.m_TransitionNodes.Count; ++i)
		{
			m_Settings.m_CurrentGraph.m_TransitionNodes[i].m_WindowRect.x+=l_Distance.x;
			m_Settings.m_CurrentGraph.m_TransitionNodes[i].m_WindowRect.y+=l_Distance.y;
		}
		for(int i=0; i<m_Settings.m_CurrentGraph.m_PointerNodes.Count; ++i)
		{
			m_Settings.m_CurrentGraph.m_PointerNodes[i].m_WindowRect.x+=l_Distance.x;
			m_Settings.m_CurrentGraph.m_PointerNodes[i].m_WindowRect.y+=l_Distance.y;
		}
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
	void AddNewNode(Event Event)
	{
		GenericMenu l_Menu=new GenericMenu();
		if(m_Settings.m_CurrentGraph!=null)
		{
			l_Menu.AddItem(new GUIContent("Add State"), false, AddStateNode); 
			l_Menu.AddSeparator("");
			l_Menu.AddItem(new GUIContent("Add Pointer"), false, AddPointerNode); 
		}
		l_Menu.ShowAsContext();
		Event.Use();
	}
	void ModifyNode(Event Event)
	{
		GenericMenu l_Menu=new GenericMenu();
		if(m_SelectedNode is StateNode _StateNode)
		{
			if(_StateNode.m_OnStateAction!=null && !m_SelectedNode.m_AlreadyExists)
			{
				l_Menu.AddItem(new GUIContent("Add Transition"), false, AddTransitionNode);
				if(!_StateNode.m_IsEntryNode) 
				{
					l_Menu.AddSeparator("");
					l_Menu.AddItem(new GUIContent("Set as Entry State"), false, SetEntryNode);
				}
				l_Menu.AddSeparator("");
			} 
			l_Menu.AddItem(new GUIContent("Delete"), false, RemoveNode);
		} 
		else if(m_SelectedNode is PointerNode)
		{
			l_Menu.AddSeparator("");
			l_Menu.AddItem(new GUIContent("Delete"), false, RemoveNode);
		} 
		else if(m_SelectedNode is TransitionNode)
		{
			if(m_SelectedNode.m_AlreadyExists || !m_SelectedNode.m_Assigned)
				l_Menu.AddDisabledItem(new GUIContent("Connect Transition"));
			else
				l_Menu.AddItem(new GUIContent("Connect Transition"), false, SetMakeTransition);
			l_Menu.AddSeparator("");
			l_Menu.AddItem(new GUIContent("Delete"), false, RemoveNode);
		}
		l_Menu.ShowAsContext();
		Event.Use();
	}
	void RemoveNode() 
	{
		if(m_SelectedNode is TransitionNode _TransitionNode)
		{
			StateNode l_EnterNode=m_Settings.m_CurrentGraph.GetStateNodeWithIndex(_TransitionNode.m_EnterNodeId);
			l_EnterNode.RemoveTransition(_TransitionNode.m_Id);
			m_Settings.m_CurrentGraph.DeleteTransitionNode(_TransitionNode.m_Id);
		}
		else
			m_Settings.m_CurrentGraph.DeleteStateNode(m_SelectedNode.m_Id);
		m_Settings.Save();
	}

	void AddTransitionNode()
	{
		TransitionNode l_TransitionNode=AddTransitionNodeOnGraph("Condition", m_MousePos);
		l_TransitionNode.m_EnterNodeId=m_SelectedNode.m_Id;
		//l_TransitionNode.m_TargetNode=-1;

		if(m_SelectedNode is StateNode _StateNode) 
		{
			//Transition l_Transition=_StateNode.AddTransition();
			//l_TransitionNode.m_TransitionId=_StateNode.m_Transitions.Count-1;
			//l_Transition.m_Id=_StateNode.m_Transitions.Count-1;

			Debug.Log(l_TransitionNode.m_Id);
			_StateNode.AddTransition(l_TransitionNode.m_Id);
		}
		m_Settings.Save();
	}
	void AddStateNode() 
	{
		StateNode l_StateNode=AddStateNodeOnGraph("State", m_MousePos);
		m_Settings.Save();
	}
	void AddPointerNode() 
	{
		AddPointerNodeOnGraph("Pointer", m_MousePos); 
		m_Settings.Save();
	}
	StateNode AddStateNodeOnGraph(string Title, Vector3 Pos)
	{
		StateNode l_Node=new StateNode();
		l_Node.m_WindowTitle=Title;
		l_Node.m_WindowRect.x=Pos.x;
		l_Node.m_WindowRect.y=Pos.y;
		l_Node.m_Id=m_Settings.m_CurrentGraph.m_Id; 
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
	TransitionNode AddTransitionNodeOnGraph(string Title, Vector3 Pos)
	{
		TransitionNode l_Node=new TransitionNode();
		l_Node.m_WindowTitle=Title;
		l_Node.m_WindowRect.x=Pos.x;
		l_Node.m_WindowRect.y=Pos.y;
		l_Node.m_Id=m_Settings.m_CurrentGraph.m_Id;
		m_Settings.m_CurrentGraph.m_Id++;
		m_Settings.m_CurrentGraph.m_TransitionNodes.Add(l_Node); 
		return l_Node;
	}
	PointerNode AddPointerNodeOnGraph(string Title, Vector3 Pos)
	{
		PointerNode l_Node=new PointerNode();
		l_Node.m_WindowTitle=Title;
		l_Node.m_WindowRect.x=Pos.x;
		l_Node.m_WindowRect.y=Pos.y;
		l_Node.m_Id=m_Settings.m_CurrentGraph.m_Id;
		m_Settings.m_CurrentGraph.m_Id++;
		m_Settings.m_CurrentGraph.m_PointerNodes.Add(l_Node); 
		return l_Node;
	}

	void SetMakeTransition() 
	{
		m_TransitionOrigin=m_SelectedNode.m_Id;
		m_MakeTransition=true;
	}
	void SetEntryNode() 
	{
		m_Settings.m_CurrentGraph.SetEntryNode(m_SelectedNode.m_Id);
		m_Settings.Save();
	}
}
