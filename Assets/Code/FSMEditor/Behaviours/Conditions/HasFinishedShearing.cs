using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/HasFinishedShearing")]
public class HasFinishedShearing : Condition
{
	public override bool CheckCondition(StateManager State, Blackboard _Blackboard)
	{
		return _Blackboard.GetValue<float>("CurrentShearTime")>=_Blackboard.GetValue<float>("MaxShearTime");
	}
}

