using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicMinion : MonoBehaviour
{
    [SerializeField] Transform target;
    public int damage=5;
    NavMeshAgent agent;
    private void Start() {
        agent= GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() {
        agent.SetDestination(target.position);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {  
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if(damageable&&collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hit"+attackDamage);
            damageable.Hit(damage);
        }
    
    }
}
