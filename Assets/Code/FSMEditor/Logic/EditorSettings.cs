using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Editor/Settings")]
public class EditorSettings : ScriptableObject
{
	public Graph m_CurrentGraph;

	[Header("Editor Window")]
	public Vector2 m_MinEditorSize;
	public Vector2 m_MaxEditorSize;

	[Header("Editor Rects")]
	public Rect m_EditorWindowRect;
	public Rect m_GraphConfigRect;
	public Rect m_NodeInspectorRect;

	[Header("Nodes")]
	public float m_StateNodeWidth;
	public float m_StateNodeHeight;
	public float m_TransitionNodeWidth;
	public float m_TransitionNodeHeight;
	public float m_NodeLabelHeight;

	[Header("Style Skin")]
	public GUISkin m_EditorSkin;

	public void Save() 
	{
		EditorUtility.SetDirty(this);
		EditorUtility.SetDirty(m_CurrentGraph);
	}

	public GUIStyle GetStateNodeLabelStyle() 
	{
		return m_EditorSkin.GetStyle("StateNodeLabel");
	}
	public GUIStyle GetTransitionNodeLabelStyle() 
	{
		return m_EditorSkin.GetStyle("TransitionNodeLabel");
	}
	public GUIStyle GetRedTextStyle() 
	{
		return m_EditorSkin.GetStyle("Label");
	}
	public GUIStyle GetCenteredTextStyle() 
	{
		return m_EditorSkin.GetStyle("CenteredText");
	}
}
