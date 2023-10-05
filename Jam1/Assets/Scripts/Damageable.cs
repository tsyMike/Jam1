using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{   

    //A character with this script on will take damage an be destroyed when its health reaches zero

    //public UnityEvent<int, Vector2>damageableHit;
    public UnityEvent<int,int> healthChanged;
    Animator animator;

    public UnityEvent<GameObject> OnHitWithReference;

    [SerializeField]
    private int _maxHealth;

    public int MaxHealth{
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth=value;
        }
    }
    [SerializeField]
    private int _health=100;
    public int Health{
        get
        {
            return _health;
        }
        set
        {
            _health=value;
            
            //If the health value changes executes an event to modify the healthbar
            healthChanged?.Invoke(Health,MaxHealth);
            if (_health<=0)
            {
                IsAlive=false;
                Pooler.Despawn(gameObject);
               
            }
        }
    }
    [SerializeField]
    private bool _isAlive=true;
    [SerializeField]
    private bool IsInvincible=false;
    private float timeSinceHit=0;
   
    public float invincivilityTimer=0.1f;

    private bool IsAlive{
        get{
            return _isAlive;
        }
        set{
            _isAlive=value;
            animator.SetBool(AnimationStrings.isAlive,value);
        }
    }

    void Awake() {
        animator= GetComponent<Animator>();
    }
    private void Update() {
        if (IsInvincible)
        {   
            if (timeSinceHit> invincivilityTimer)
            {   
                IsInvincible=false;
                timeSinceHit=0;
            }
            timeSinceHit+=Time.deltaTime;
        }  
       //Hit(10);
         
    }
    public void Hit(int damage){
        if (IsAlive&& !IsInvincible)
        {
            Health-=damage;
            IsInvincible=true;

            //Invokes a method to display the damage on screen
            //CharacterEvents.characterDamaged.Invoke(gameObject,damage);
        }
    }

    //Damage with Knockback
    private float strenght = 50, delay = 0.15f;
    [SerializeField] 
    private Rigidbody2D rb2d; 
    public void Hit(int damage, GameObject sender){
        if (IsAlive&& !IsInvincible)
        {   
            Vector2 direction=(transform.position-sender.transform.position);
            rb2d.AddForce(direction*strenght,ForceMode2D.Impulse);
            StartCoroutine(Reset());
            Health-=damage;
            IsInvincible=true;
            
            //Invokes a method to display the damage on screen
            //CharacterEvents.characterDamaged.Invoke(gameObject,damage);
        }
    }
    private IEnumerator Reset(){
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
    }
    
    
}
