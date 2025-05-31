using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

[CreateAssetMenu(menuName = "Actions/ReachSheep")]
public class ReachShearableSheep : StateAction
{
	bool m_TargetAssigned;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		if(!m_TargetAssigned) 
		{
			List<GameObject> l_Sheeps=_Blackboard.GetValue<List<GameObject>>("Sheeps");
			for(int i=0; i<l_Sheeps.Count; ++i) 
			{
				if(l_Sheeps[i].GetComponent<SheepBlackboard>().GetValue<bool>("IsShearable")) 
				{
					_Blackboard.SetValue<GameObject>("TargetSheep", l_Sheeps[i]);
					m_TargetAssigned=true;
				}
			}
		}
		else 
		{ 
			Vector3 l_TargetPos=_Blackboard.GetValue<GameObject>("TargetSheep").transform.position;
			l_TargetPos.y=_Blackboard.transform.position.y;
			_Blackboard.transform.forward=(l_TargetPos-_Blackboard.transform.position).normalized;
			_Blackboard.transform.position=Vector3.MoveTowards(_Blackboard.transform.position, l_TargetPos, _Blackboard.GetValue<float>("Speed")*Time.deltaTime);
		}
	}
}
