using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/SetAnimatorTrigger")]
public class SetAnimatorTrigger : StateAction
{
	public string m_ParameterName;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		_Blackboard.GetValue<Animator>("Animator").SetTrigger(m_ParameterName);
	}
}
