using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/PatrolPoints")]
public class PatrolPoints : StateAction
{
	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		int l_Index=_Blackboard.GetValue<int>("CurrentPatrolPoint");
		List<Transform> l_PointsList=_Blackboard.GetValue<List<Transform>>("PatrolPoints");
		Vector3 l_NextPoint=l_PointsList[l_Index].position;

		Vector3 l_Direction=l_NextPoint-_Blackboard.transform.position;
		_Blackboard.transform.position+=l_Direction.normalized*_Blackboard.GetValue<float>("Speed")*Time.deltaTime;
		_Blackboard.transform.forward=l_Direction;
		float l_Distance=l_Direction.magnitude;

		if(l_Distance<0.25f)
		{
			l_Index++;
			if(l_Index>=l_PointsList.Count)
				l_Index=0;
			_Blackboard.SetValue<int>("CurrentPatrolPoint", l_Index);
		}
	}
}
