using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/WanderAroundPoint2D")]
public class WanderAroundPoint2D : StateAction
{
	public float m_MaxDistanceFromPoint;
	public float m_MaxTimeSinceLastPos;
	public float m_MinTimeSinceLastPos;
	float m_NextTimeSinceLastPos;
	float m_LastPosTimer;
	Vector2 m_TargetPos;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		Transform l_WanderPoint=_Blackboard.GetValue<Transform>("WanderPoint");
		if(l_WanderPoint!=null) 
		{
			m_LastPosTimer+=Time.deltaTime;
			if(m_LastPosTimer>=m_NextTimeSinceLastPos) 
			{
				Vector2 l_RandomPoint=Random.insideUnitCircle*m_MaxDistanceFromPoint;
                m_TargetPos=new Vector2(l_WanderPoint.position.x, l_WanderPoint.position.y)+l_RandomPoint;
				m_NextTimeSinceLastPos=Random.Range(m_MinTimeSinceLastPos, m_MaxTimeSinceLastPos);
				m_LastPosTimer=0.0f;
			}
			else
			{
				if(Vector3.Distance(_Blackboard.transform.position, m_TargetPos)>0.1f) 
				{	
					Vector2 l_Dir=(m_TargetPos-new Vector2(_Blackboard.transform.position.x, _Blackboard.transform.position.y)).normalized;
					l_Dir.y=0.0f;
					_Blackboard.transform.right=l_Dir;
					_Blackboard.transform.position=Vector2.MoveTowards(_Blackboard.transform.position, m_TargetPos, _Blackboard.GetValue<float>("Speed")*Time.deltaTime);
				}
			}
		}
	}
}
