using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/ShearSheep")]
public class ShearSheep : StateAction
{
	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		_Blackboard.SetValue<float>("CurrentShearTime", _Blackboard.GetValue<float>("CurrentShearTime")+Time.deltaTime);

		if(_Blackboard.GetValue<float>("CurrentShearTime")>=_Blackboard.GetValue<float>("MaxShearTime"))
		{ 
			SheepBlackboard l_Sheep =_Blackboard.GetValue<GameObject>("TargetSheep").GetComponent<SheepBlackboard>();
			l_Sheep.GetValue<GameObject>("Wool").SetActive(false);
			l_Sheep.SetValue<bool>("IsShearable", false);
			l_Sheep.SetValue<bool>("IsBeingSheared", false);
		}
	}
}
