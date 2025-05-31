using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/IsSheared")]
public class IsSheared : Condition
{
	public override bool CheckCondition(StateManager State, Blackboard _Blackboard)
	{
		return !_Blackboard.GetValue<bool>("IsShearable");
	}
}

