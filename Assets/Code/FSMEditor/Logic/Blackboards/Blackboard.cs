using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
	private Dictionary<string, object> m_AgentInfo=new Dictionary<string, object>();

	[Header("Properties")]
	public float m_Health;
	public float m_Damage;
	public float m_Speed;
	public List<Transform> m_PatrolPoints=new List<Transform>();

    protected virtual void Awake()
    {
        SetValue<float>("Health", m_Health);
        SetValue<float>("Damage", m_Damage);
        SetValue<float>("Speed", m_Speed);
        //SetValue<List<Transform>>("PatrolPoints", m_PatrolPoints);
    }

    public void SetValue<T>(string Key, T Value)
    {
		if(m_AgentInfo.ContainsKey(Key)) 
			m_AgentInfo[Key]=Value;
		else
			m_AgentInfo.Add(Key, Value);
    }

    public T GetValue<T>(string Key)
    {
		if(m_AgentInfo.ContainsKey(Key)) 
			return (T)m_AgentInfo[Key];
		else
			return default;
    }

    public bool HasKey(string Key) 
    {
        return m_AgentInfo.ContainsKey(Key);
    } 

    public void RemoveKey(string key) 
    {
        m_AgentInfo.Remove(key);
    }
}
