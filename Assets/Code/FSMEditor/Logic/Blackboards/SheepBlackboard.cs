using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBlackboard : Blackboard
{
    [Header("SHEEP PROPERTIES")]
    public GameObject m_Wool;
    bool m_IsShearable;
    bool m_IsBeingSheared;
    public Transform m_WanderPoint;
    public List<Transform> m_EatPoints;
    public GameObject m_Farmer;
    public float m_MaxHunger;
    public float m_HungerIncrease;
    public float m_HungerDecrease;


    protected override void Awake()
    {
        base.Awake();
        SetValue<GameObject>("Wool", m_Wool);
        SetValue<bool>("IsShearable", m_IsShearable);
        SetValue<bool>("IsBeingSheared", m_IsBeingSheared);
        SetValue<Transform>("WanderPoint", m_WanderPoint);
        SetValue<List<Transform>>("EatPoints", m_EatPoints);
        SetValue<Transform>("TargetEatPoint", null);
        SetValue<GameObject>("Farmer", m_Farmer);
        SetValue<float>("MaxHunger", m_MaxHunger);
        SetValue<float>("HungerIncrease", m_HungerIncrease);
        SetValue<float>("HungerDecrease", m_HungerDecrease);
    }
}
