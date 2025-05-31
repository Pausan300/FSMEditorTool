using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/HasReachedSheep")]
public class HasReachedSheep : Condition
{
	public override bool CheckCondition(StateManager State, Blackboard _Blackboard)
	{
		return Vector3.Distance(_Blackboard.GetValue<GameObject>("TargetSheep").transform.position, _Blackboard.transform.position)<=0.5f;
	}
}

