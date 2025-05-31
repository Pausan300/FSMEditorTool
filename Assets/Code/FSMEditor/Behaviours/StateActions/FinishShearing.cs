using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/FinishShearing")]
public class FinishShearing : StateAction
{
	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		_Blackboard.SetValue<GameObject>("TargetSheep", null);
		_Blackboard.SetValue<float>("CurrentShearTime", 0.0f);
	}
}
