using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class Node 
{
	public int m_Id;

    public Rect m_WindowRect;

	public bool m_Assigned;


	public virtual void DrawWindow() { }
	public virtual void DrawLabel() { }
}