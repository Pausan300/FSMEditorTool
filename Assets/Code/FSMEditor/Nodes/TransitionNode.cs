using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[System.Serializable]
public class TransitionNode : Node
{
	public int m_EnterNodeId=-1;
	public int m_TargetNodeId=-1;

	public float m_ExtraWindowHeight;

	ReorderableList m_ConditionsList;
	public List<Condition> m_Conditions=new List<Condition>();


	public override void DrawWindow()
	{
		if(m_EnterNodeId==-1)
			return;
		
		GUILayout.Space(10.0f); 

		if(m_Conditions.Count<=0)
		{
			EditorGUILayout.LabelField("No Condition assigned");
			m_Assigned=false;
		}
		else
		{
			bool l_AllConditionsAssigned=true;
			for(int i=0; i<m_Conditions.Count; ++i) 
			{
				if(m_Conditions[i]==null)
					l_AllConditionsAssigned=false;
			}
			m_Assigned=l_AllConditionsAssigned;
        }

		m_ConditionsList=new ReorderableList(m_Conditions, typeof(Condition), false, true, true, true);

        // Cabecera de la lista
        m_ConditionsList.drawHeaderCallback=(Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Conditions");
        };

        m_ConditionsList.drawElementCallback=(Rect rect, int index, bool isActive, bool isFocused) =>
        {
            m_Conditions[index]=(Condition)EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), m_Conditions[index], typeof(Condition), true);
        };
        m_ConditionsList.onAddCallback=(ReorderableList list) =>
        {
            m_Conditions.Add(null);
			if(list.count>1)
				m_ExtraWindowHeight+=22.5f;
        };
        m_ConditionsList.onRemoveCallback=(ReorderableList list) =>
        {
			if(list.count>1)
				m_ExtraWindowHeight-=22.5f;
            m_Conditions.RemoveAt(list.index);
        };
		m_ConditionsList.DoLayoutList();

		//m_Condition=(Condition)EditorGUILayout.ObjectField(m_Condition, typeof(Condition), false);
	}
	public void DrawLine()
	{
		Rect l_OwnRect=m_WindowRect;
		l_OwnRect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
		l_OwnRect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;

		StateNode l_EnterNode=FSMEditor.m_Settings.m_CurrentGraph.GetStateNodeWithIndex(m_EnterNodeId);

		Color l_TargetColor=Color.green;
		if(!m_Assigned || !l_EnterNode.m_Assigned)
			l_TargetColor=Color.red;

		Rect l_PreviousRect = l_EnterNode.m_WindowRect;
		l_PreviousRect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
		l_PreviousRect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;

		FSMEditor.DrawNodeLine(l_PreviousRect, l_OwnRect, l_TargetColor);
		
		StateNode l_TargetNode=FSMEditor.m_Settings.m_CurrentGraph.GetStateNodeWithIndex(m_TargetNodeId);
		if(l_TargetNode==null)
			return;

		Rect l_NextRect = l_TargetNode.m_WindowRect;
		l_NextRect.y-=FSMEditor.m_Settings.m_NodeLabelHeight;
		l_NextRect.height+=FSMEditor.m_Settings.m_NodeLabelHeight;

		l_TargetColor=Color.green;
		if(!l_TargetNode.m_Assigned /*|| m_Conditions.Count==0*/)
			l_TargetColor=Color.red;
		//if(m_Conditions.Count>0) 
		//{
		//	bool l_AllConditionsNull=true;
		//	for(int i=0; i<m_Conditions.Count; ++i) 
		//	{
		//		if(m_Conditions[i]!=null)
		//			l_AllConditionsNull=false;
		//	}
		//	if(l_AllConditionsNull)
		//		l_TargetColor=Color.red;
		//}

		FSMEditor.DrawNodeLine(l_OwnRect, l_NextRect, l_TargetColor);
	}
    public override void DrawLabel()
    {
        GUI.Box(new Rect(m_WindowRect.x, m_WindowRect.y-FSMEditor.m_Settings.m_NodeLabelHeight, m_WindowRect.width, FSMEditor.m_Settings.m_NodeLabelHeight),
			"TRANSITION", FSMEditor.m_Settings.GetTransitionNodeLabelStyle());
    }
}
