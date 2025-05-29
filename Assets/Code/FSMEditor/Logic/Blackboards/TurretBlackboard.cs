using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBlackboard : Blackboard
{
    [Header("TURRET PROPERTIES")]
    public GameObject m_Projectile;
    public Transform m_TurretHead;
    public float m_ProjectileSpeed;

    protected override void Awake()
    {
        base.Awake();
        SetValue<GameObject>("Projectile", m_Projectile);
        SetValue<Transform>("TurretHead", m_TurretHead);
        SetValue<float>("ProjectileSpeed", m_ProjectileSpeed);
    }
}
