using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

[CreateAssetMenu(menuName = "Actions/ReachPoint")]
public class ReachRandomEatPoint : StateAction
{
	bool m_TargetAssigned;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		if(!m_TargetAssigned) 
		{
			List<Transform> l_PossiblePoints=_Blackboard.GetValue<List<Transform>>("EatPoints");
			_Blackboard.SetValue<Transform>("TargetEatPoint", l_PossiblePoints[Random.Range(0, l_PossiblePoints.Count)]);
			m_TargetAssigned=true;
		}
		else 
		{ 
			Vector3 l_TargetPos=_Blackboard.GetValue<Transform>("TargetEatPoint").position;
			_Blackboard.transform.forward=(l_TargetPos-_Blackboard.transform.position).normalized;
			_Blackboard.transform.position=Vector3.MoveTowards(_Blackboard.transform.position, l_TargetPos, _Blackboard.GetValue<float>("Speed")*Time.deltaTime);
		}
	}
}
