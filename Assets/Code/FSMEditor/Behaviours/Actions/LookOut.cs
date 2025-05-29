using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/LookOut")]
public class LookOut : StateAction
{
	public float m_RotationSpeed;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		Transform l_RotatingTransform=_Blackboard.GetValue<Transform>("TurretHead");
		l_RotatingTransform.Rotate(0.0f, m_RotationSpeed*Time.deltaTime, 0.0f);
	}
}
