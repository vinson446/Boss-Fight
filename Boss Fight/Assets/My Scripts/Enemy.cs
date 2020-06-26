using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 30f;
    public float damage = 10f;

    public Transform goal;

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        agent.SetDestination(goal.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.TakeDamage(damage);
            Debug.Log("Player was damaged");
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
