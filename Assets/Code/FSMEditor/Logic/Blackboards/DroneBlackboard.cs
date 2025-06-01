using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBlackboard : Blackboard, ITakeDamage
{
    [Header("DRONE PROPERTIES")]
	public float m_MaxHealth;
    public List<Transform> m_PatrolPoints;
    public List<Transform> m_LaserPoints;
    public List<LineRenderer> m_Lasers;

    protected override void Awake()
    {
        base.Awake();
        SetValue<float>("MaxHealth", m_MaxHealth);
        SetValue<float>("CurrentHealth", m_MaxHealth);
        SetValue<List<Transform>>("PatrolPoints", m_PatrolPoints);
        SetValue<List<Transform>>("LaserPoints", m_LaserPoints);
        SetValue<List<LineRenderer>>("Lasers", m_Lasers);
        SetValue<int>("CurrentPatrolPoint", 0);
        SetValue<Transform>("Player", GameObject.FindGameObjectWithTag("Player").transform);
    }

    public void TakeDamage(float Damage)
    {
        SetValue<float>("CurrentHealth", GetValue<float>("CurrentHealth")-Damage);
        if(GetValue<float>("CurrentHealth")<=0.0f)
            Destroy(gameObject);
    }
}
