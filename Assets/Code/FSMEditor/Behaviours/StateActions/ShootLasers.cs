using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/ShootLasers")]
public class ShootLasers : StateAction
{
	public float m_FireRate;
	float m_FireRateTimer;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		Transform l_Player=_Blackboard.GetValue<Transform>("Player");
		_Blackboard.transform.LookAt(l_Player);

		m_FireRateTimer+=Time.deltaTime;
		if(m_FireRateTimer>=m_FireRate) 
		{
			List<LineRenderer> l_Lasers=_Blackboard.GetValue<List<LineRenderer>>("Lasers");
			List<Transform> l_LaserPoints=_Blackboard.GetValue<List<Transform>>("LaserPoints");
			for(int i=0; i<l_Lasers.Count; ++i) 
			{
				l_Lasers[i].SetPosition(0, l_LaserPoints[i].position);
				l_Lasers[i].SetPosition(1, l_Player.position+Vector3.up);
			}
			l_Player.GetComponent<ITakeDamage>().TakeDamage(_Blackboard.GetValue<float>("Damage"));
			m_FireRateTimer=0.0f;
			_Blackboard.StartCoroutine(HideLasers(_Blackboard));
		}
	}

	public IEnumerator HideLasers(Blackboard _Blackboard)
    {
		yield return new WaitForSeconds(0.25f);
        List<LineRenderer> l_Lasers=_Blackboard.GetValue<List<LineRenderer>>("Lasers");
		List<Transform> l_LaserPoints=_Blackboard.GetValue<List<Transform>>("LaserPoints");
		for(int i=0; i<l_Lasers.Count; ++i) 
		{
			l_Lasers[i].SetPosition(0, Vector3.zero);
			l_Lasers[i].SetPosition(1, Vector3.zero);
		}
    }
}
