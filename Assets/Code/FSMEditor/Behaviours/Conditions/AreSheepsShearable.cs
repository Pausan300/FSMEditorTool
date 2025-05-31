using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/AreSheepsShearable")]
public class AreSheepsShearable : Condition
{
	public override bool CheckCondition(StateManager State, Blackboard _Blackboard)
	{
		List<GameObject> l_Sheeps=_Blackboard.GetValue<List<GameObject>>("Sheeps");
		for(int i=0; i<l_Sheeps.Count; ++i) 
		{
			if(l_Sheeps[i].GetComponent<SheepBlackboard>().GetValue<bool>("IsShearable"))
				return true;
		}
		return false;
	}
}

