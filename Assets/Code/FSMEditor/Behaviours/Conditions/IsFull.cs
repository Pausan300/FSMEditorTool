using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/IsFull")]
public class IsFull : Condition
{
	public override bool CheckCondition(StateManager State, Blackboard _Blackboard)
	{
		return _Blackboard.GetValue<float>("Hunger")<=0.0f;
	}
}

