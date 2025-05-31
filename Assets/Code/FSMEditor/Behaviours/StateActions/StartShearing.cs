using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/StartShearing")]
public class StartShearing : StateAction
{
	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		SheepBlackboard l_Sheep=_Blackboard.GetValue<GameObject>("TargetSheep").GetComponent<SheepBlackboard>();
		l_Sheep.SetValue<bool>("IsBeingSheared", true);
	}
}
