using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class Node 
{
	public int m_Id;

	//public enum TNodeType 
	//{
	//	STATE,
	//	TRANSITION
	//}
	//public TNodeType m_NodeType;

    public Rect m_WindowRect;
	public string m_WindowTitle;

	public bool m_AlreadyExists;
	public bool m_Assigned;

	public virtual void DrawWindow() { }
	public virtual void DrawLabel() { }
}