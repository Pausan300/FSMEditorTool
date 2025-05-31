using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/HasReachedWatchPoint")]
public class HasReachedWatchPoint : Condition
{
	public override bool CheckCondition(StateManager State, Blackboard _Blackboard)
	{
		return Vector3.Distance(_Blackboard.GetValue<Transform>("WatchPoint").position, _Blackboard.transform.position)<=0.1f;
	}
}

