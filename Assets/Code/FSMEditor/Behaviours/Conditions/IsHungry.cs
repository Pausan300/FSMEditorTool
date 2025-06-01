using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/IsHungry")]
public class IsHungry : Condition
{
	public override bool CheckCondition(StateManager State, Blackboard _Blackboard)
	{
		return _Blackboard.GetValue<float>("Hunger")>=_Blackboard.GetValue<float>("MaxHunger");
	}
}

