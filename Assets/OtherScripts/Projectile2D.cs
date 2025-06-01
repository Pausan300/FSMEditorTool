using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    float m_Damage;
    float m_Speed;
    Vector2 m_Direction;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 l_Dir=m_Direction;
	    l_Dir.y=0.0f;
	    transform.right=l_Dir;
        transform.position+=new Vector3(m_Direction.x, m_Direction.y, transform.position.z)*m_Speed*Time.deltaTime;
    }
    public void SetProjectileStats(float Damage, float Speed, Vector2 Direction)
    {
        m_Damage=Damage;
        m_Speed=Speed;
        m_Direction=Direction.normalized;
        transform.forward=Direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        {
            if(collision.transform.TryGetComponent(out ITakeDamage Player))
                Player.TakeDamage(m_Damage);
        }
        if(!collision.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
