using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Eat")]
public class Eat : StateAction
{
	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		_Blackboard.SetValue<float>("Hunger", _Blackboard.GetValue<float>("Hunger")-_Blackboard.GetValue<float>("HungerDecrease")*Time.deltaTime);
	}
}
