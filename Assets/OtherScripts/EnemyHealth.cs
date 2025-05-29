using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    public float m_MaxHealth;
    float m_CurrentHealth;

    void Start()
    {
        m_CurrentHealth=m_MaxHealth;
    }
    void Update()
    {
        
    }
    public void TakeDamage(float Damage)
    {
        m_CurrentHealth-=Damage;
        if(m_CurrentHealth<=0.0f)
            Destroy(gameObject);
    }
}
