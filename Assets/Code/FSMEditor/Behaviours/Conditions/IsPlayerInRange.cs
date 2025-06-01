using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/IsPlayerInRange")]
public class IsPlayerInRange : Condition
{
	public float m_MaxRange;
	public bool m_OutOfRange;

	public override bool CheckCondition(StateManager State, Blackboard _Blackboard)
	{
		Transform l_Player=_Blackboard.GetValue<Transform>("Player");
		if(m_OutOfRange)
			return Vector3.Distance(l_Player.position, _Blackboard.transform.position)>m_MaxRange;
		else
			return Vector3.Distance(l_Player.position, _Blackboard.transform.position)<=m_MaxRange;
	}
}

