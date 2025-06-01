using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile3D : MonoBehaviour
{
    float m_Damage;
    float m_Speed;
    Vector3 m_Direction;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position+=m_Direction*m_Speed*Time.deltaTime;
    }

    public void SetProjectileStats(float Damage, float Speed, Vector3 Direction) 
    {
        m_Damage=Damage;
        m_Speed=Speed;
        m_Direction=Direction.normalized;
        transform.forward=Direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            if(other.transform.TryGetComponent(out ITakeDamage Player))
                Player.TakeDamage(m_Damage);
        }
        if(!other.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
