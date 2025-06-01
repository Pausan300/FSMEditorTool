using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspidHunterBlackboard : Blackboard, ITakeDamage
{
    [Header("ASPID HUNTER PROPERTIES")]
	public float m_MaxHealth;
    public Transform m_WanderPoint;
    public GameObject m_Projectile;
	public float m_ProjectileSpeed;

    protected override void Awake()
    {
        base.Awake();
        SetValue<float>("MaxHealth", m_MaxHealth);
        SetValue<float>("CurrentHealth", m_MaxHealth);
        SetValue<Transform>("WanderPoint", m_WanderPoint);
        SetValue<GameObject>("Projectile", m_Projectile);
        SetValue<float>("ProjectileSpeed", m_ProjectileSpeed);
        SetValue<Transform>("Player", GameObject.FindGameObjectWithTag("Player").transform);
        SetValue<Animator>("Animator", GetComponent<Animator>());
    }

    public void TakeDamage(float Damage)
    {
        SetValue<float>("CurrentHealth", GetValue<float>("CurrentHealth")-Damage);
        StartCoroutine(DamageSprite());
        if(GetValue<float>("CurrentHealth")<=0.0f)
            Destroy(gameObject);
    }

    IEnumerator DamageSprite() 
    {
        GetComponent<SpriteRenderer>().color=Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color=Color.white;
    }
}
