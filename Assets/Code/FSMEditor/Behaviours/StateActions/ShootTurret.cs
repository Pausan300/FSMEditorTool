using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/ShootTurret")]
public class ShootTurret : StateAction
{
	public float m_FireRate;
	float m_FireRateTimer;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		Transform l_Player=_Blackboard.GetValue<Transform>("Player");
		_Blackboard.GetValue<Transform>("TurretHead").LookAt(l_Player);

		m_FireRateTimer+=Time.deltaTime;
		if(m_FireRateTimer>=m_FireRate) 
		{
			GameObject l_Projectile=Instantiate(_Blackboard.GetValue<GameObject>("Projectile"), _Blackboard.GetValue<Transform>("TurretHead").position+Vector3.up, _Blackboard.transform.rotation);
			l_Projectile.GetComponent<Projectile3D>().SetProjectileStats(_Blackboard.GetValue<float>("Damage"), _Blackboard.GetValue<float>("ProjectileSpeed"), (l_Player.transform.position-_Blackboard.transform.position).normalized);
			m_FireRateTimer=0.0f;
		}
	}
}
