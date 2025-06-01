using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/PlayAnimation")]
public class PlayAnimation : StateAction
{
	public AnimationClip m_Anim;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		_Blackboard.GetValue<Animation>("Animation").Play(m_Anim.name);
	}
}
