using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBlackboard : Blackboard, ITakeDamage
{
    [Header("TURRET PROPERTIES")]
	public float m_MaxHealth;
    public GameObject m_Projectile;
    public Transform m_TurretHead;
    public float m_ProjectileSpeed;


    protected override void Awake()
    {
        base.Awake();
        SetValue<float>("MaxHealth", m_MaxHealth);
        SetValue<float>("CurrentHealth", m_MaxHealth);
        SetValue<GameObject>("Projectile", m_Projectile);
        SetValue<Transform>("TurretHead", m_TurretHead);
        SetValue<float>("ProjectileSpeed", m_ProjectileSpeed);
        SetValue<Transform>("Player", GameObject.FindGameObjectWithTag("Player").transform);
    }

    public void TakeDamage(float Damage)
    {
        SetValue<float>("CurrentHealth", GetValue<float>("CurrentHealth")-Damage);
        if(GetValue<float>("CurrentHealth")<=0.0f)
            Destroy(gameObject);
    }
}
