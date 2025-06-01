using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingHuskBlackboard : Blackboard, ITakeDamage
{
    [Header("WANDERING HUSK PROPERTIES")]
	public float m_MaxHealth;
    public List<Transform> m_PatrolPoints;

    protected override void Awake()
    {
        base.Awake();
        SetValue<float>("MaxHealth", m_MaxHealth);
        SetValue<float>("CurrentHealth", m_MaxHealth);
        SetValue<List<Transform>>("PatrolPoints", m_PatrolPoints);
        SetValue<int>("CurrentPatrolPoint", 0);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
            collision.gameObject.GetComponent<ITakeDamage>().TakeDamage(m_Damage);
    }
}
