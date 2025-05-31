using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

[CreateAssetMenu(menuName = "Actions/GrowWool")]
public class GrowWool : StateAction
{
	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		_Blackboard.GetValue<GameObject>("Wool").SetActive(true);
		_Blackboard.SetValue<bool>("IsShearable", true);
	}
}
