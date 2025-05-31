using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerBlackboard : Blackboard
{
    [Header("FARMER PROPERTIES")]
    public Transform m_WatchPoint;
    public List<GameObject> m_Sheeps;
    public float m_MaxShearTime;

    protected override void Awake()
    {
        base.Awake();
        SetValue<Transform>("WatchPoint", m_WatchPoint);
        SetValue<List<GameObject>>("Sheeps", m_Sheeps);
        SetValue<GameObject>("TargetSheep", null);
        SetValue<float>("MaxShearTime", m_MaxShearTime);
        SetValue<float>("CurrentShearTime", 0.0f);
        SetValue<Animation>("Animation", GetComponent<Animation>());
    }
}
