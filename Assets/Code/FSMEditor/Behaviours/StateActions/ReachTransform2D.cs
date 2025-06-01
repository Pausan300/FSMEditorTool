using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

[CreateAssetMenu(menuName = "Actions/ReachTransform2D")]
public class ReachTransform2D : StateAction
{
	public string m_TransformKey;
	public float m_ExtraSpeed;
	public bool m_IgnoreY;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		Vector3 l_TargetPos=_Blackboard.GetValue<Transform>(m_TransformKey).position;
		if(m_IgnoreY)
			l_TargetPos.y=_Blackboard.transform.position.y;

		Vector3 l_Direction=l_TargetPos-_Blackboard.transform.position;
		l_Direction.Normalize();
		_Blackboard.transform.position=Vector2.MoveTowards(_Blackboard.transform.position, l_TargetPos, (_Blackboard.GetValue<float>("Speed")+m_ExtraSpeed)*Time.deltaTime);
		_Blackboard.transform.right=l_Direction;
	}
}
