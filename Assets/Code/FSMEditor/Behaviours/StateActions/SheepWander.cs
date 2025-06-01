using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/SheepWander")]
public class SheepWander : StateAction
{
	public float m_MaxDistanceFromPoint;
	public float m_MaxTimeSinceLastPos;
	public float m_MinTimeSinceLastPos;
	float m_NextTimeSinceLastPos;
	float m_LastPosTimer;
	Vector3 m_TargetPos;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		Transform l_WanderPoint=_Blackboard.GetValue<Transform>("WanderPoint");
		if(l_WanderPoint!=null) 
		{
			_Blackboard.SetValue<float>("Hunger", _Blackboard.GetValue<float>("Hunger")+_Blackboard.GetValue<float>("HungerIncrease")*Time.deltaTime);
			m_LastPosTimer+=Time.deltaTime;
			if(m_LastPosTimer>=m_NextTimeSinceLastPos) 
			{
				Vector2 l_RandomPoint=Random.insideUnitCircle*m_MaxDistanceFromPoint;
                m_TargetPos=l_WanderPoint.position+new Vector3(l_RandomPoint.x, 0.0f, l_RandomPoint.y);
				m_NextTimeSinceLastPos=Random.Range(m_MinTimeSinceLastPos, m_MaxTimeSinceLastPos);
				m_LastPosTimer=0.0f;
			}
			else
			{
				if(Vector3.Distance(_Blackboard.transform.position, m_TargetPos)>0.1f) 
				{
					_Blackboard.transform.forward=(m_TargetPos-_Blackboard.transform.position).normalized;
					_Blackboard.transform.position=Vector3.MoveTowards(_Blackboard.transform.position, m_TargetPos, _Blackboard.GetValue<float>("Speed")*Time.deltaTime);
				}
			}
		}
	}
}
