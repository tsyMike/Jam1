using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicMinion : MonoBehaviour
{
    [SerializeField] Transform target;
    public int damage=5;
    NavMeshAgent agent;
    private Animator anim;

    private SpriteRenderer sr;
    private void Start() {
        agent= GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim= GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        agent.SetDestination(target.position);
        Vector2 targetVector = target.position;
        FaceMovement(targetVector);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {  
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if(damageable&&collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hit"+attackDamage);
            damageable.Hit(damage);
            anim.SetBool("isAttack",true);
        }
    
    }
    public void FaceMovement(Vector2 targetVector)
    {

        //newPosition=transform.position;
        if (targetVector.x - transform.position.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if (targetVector.x - transform.position.x < 0 && isFacingRight)
            isFacingRight = false;

        //pastPosition = newPosition;
    }


    public bool _isFacingRight = true;

    public bool isFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                //posit.localScale *= new Vector2(-1, 1);
                sr.flipX=!sr.flipX;
            }
            _isFacingRight = value;
        }
    } 
}
