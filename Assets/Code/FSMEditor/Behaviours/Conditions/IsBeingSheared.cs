using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/IsBeingSheared")]
public class IsBeingSheared : Condition
{
	public override bool CheckCondition(StateManager State, Blackboard _Blackboard)
	{
		return _Blackboard.GetValue<bool>("IsBeingSheared");
	}
}

