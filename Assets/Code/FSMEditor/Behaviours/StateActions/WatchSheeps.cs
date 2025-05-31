using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

[CreateAssetMenu(menuName = "Actions/WatchSheeps")]
public class WatchSheeps : StateAction
{
	public float m_MaxTimeLookingAtSheep;
	float m_LookingTimer;
	Transform m_Target;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		m_LookingTimer+=Time.deltaTime;
		if(m_LookingTimer>=m_MaxTimeLookingAtSheep) 
		{
			List<GameObject> l_PossibleSheeps=_Blackboard.GetValue<List<GameObject>>("Sheeps");
			m_Target=l_PossibleSheeps[Random.Range(0, l_PossibleSheeps.Count)].transform;
			m_LookingTimer=0.0f;
		}
		else 
		{
			_Blackboard.transform.LookAt(m_Target);
			_Blackboard.transform.eulerAngles=new Vector3(0.0f, _Blackboard.transform.eulerAngles.y, 0.0f);
		}
	}
}
