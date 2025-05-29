using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/IsPlayerInRange")]
public class IsPlayerInRange : Condition
{
	public float m_DetectDistance;

	public override bool CheckCondition(StateManager State, Blackboard _Blackboard)
	{
		GameObject l_Player=GameObject.Find("Character");
		return Vector3.Distance(l_Player.transform.position, _Blackboard.transform.position)<=m_DetectDistance;
	}
}

