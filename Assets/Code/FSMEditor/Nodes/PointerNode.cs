using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CreateAssetMenu(menuName = "Editor/Nodes/Pointer Node")]
//public class PointerNode : DrawNode
[System.Serializable]
public class PointerNode : Node
{
	//public override void DrawWindow(Node Node)
	public override void DrawWindoww()
	{
		//Node.m_StateReferences.m_CurrentState=(State)EditorGUILayout.ObjectField(Node.m_StateReferences.m_CurrentState, typeof(State), false);
		//Node.m_StateReferences.m_OnStateAction=(StateAction)EditorGUILayout.ObjectField(Node.m_StateReferences.m_OnStateAction, typeof(StateAction), false);
		m_StateReferences.m_OnStateAction=(StateAction)EditorGUILayout.ObjectField(m_StateReferences.m_OnStateAction, typeof(StateAction), false);
		//Node.m_Assigned=Node.m_StateReferences.m_CurrentState!=null;
		//Node.m_Assigned=Node.m_StateReferences.m_OnStateAction!=null; 
		m_Assigned=m_StateReferences.m_OnStateAction!=null; 
	}

	//public override void DrawLine()
	//{
		
	//}

    public override void DrawLabell()
    {
        //GUI.Box(new Rect(Node.m_WindowRect.x, Node.m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, Node.m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight), "POINTER", FSMEditor.m_Settings.GetStateNodeLabelStyle());
        GUI.Box(new Rect(m_WindowRect.x, m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight), "POINTER", FSMEditor.m_Settings.GetStateNodeLabelStyle());
    }
}
