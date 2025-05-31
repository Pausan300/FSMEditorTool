using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/PatrolPoints")]
public class PatrolPoints : StateAction
{
	public override void Execute(StateManager States, Blackboard Blackboard)
	{
		//Vector3 l_Direction=Blackboard.m_NextPatrolPoint.position-States.m_Transform.position;
		//States.m_Transform.position+=l_Direction.normalized*Blackboard.m_Speed;
		//States.m_Transform.forward=l_Direction;
		//float l_Distance=l_Direction.magnitude;
		//if(l_Distance<0.5f)
		//{
		//	int l_Index=Blackboard.m_PatrolPoints.IndexOf(Blackboard.m_NextPatrolPoint);
		//	l_Index++;
		//	if(l_Index>=Blackboard.m_PatrolPoints.Count)
		//		l_Index=0;
		//	Blackboard.m_NextPatrolPoint=Blackboard.m_PatrolPoints[l_Index];
		//}
		//Blackboard.m_CurrentHunger-=Time.deltaTime*Blackboard.m_HungerLossPerSecond;
	}
}
