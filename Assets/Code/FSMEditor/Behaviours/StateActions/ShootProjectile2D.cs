using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/ShootProjectile2D")]
public class ShootProjectile2D : StateAction
{
	public float m_FireRate;
	float m_FireRateTimer;

	public override void Execute(StateManager States, Blackboard _Blackboard)
	{
		GameObject l_Player=_Blackboard.GetValue<Transform>("Player").gameObject;

		m_FireRateTimer+=Time.deltaTime;
		if(m_FireRateTimer>=m_FireRate) 
		{
			GameObject l_Projectile=Instantiate(_Blackboard.GetValue<GameObject>("Projectile"), _Blackboard.transform.position, _Blackboard.transform.rotation);
			l_Projectile.GetComponent<Projectile2D>().SetProjectileStats(_Blackboard.GetValue<float>("Damage"), _Blackboard.GetValue<float>("ProjectileSpeed"), (l_Player.transform.position-_Blackboard.transform.position).normalized);
			m_FireRateTimer=0.0f;
		}
	}
}
