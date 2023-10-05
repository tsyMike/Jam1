using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{
    public GameObject target=null;
    public int HealthValue=3;
    public int DamageValue=3;
    // Start is called before the first frame update
    Animator anim;

    void Start()
    {   
        anim=gameObject.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        target.GetComponent<Damageable>().MaxHealth+=HealthValue;
        target.GetComponent<Damageable>().Health+=HealthValue;
        target.GetComponent<Player>().DashDMG+=DamageValue;
        Player.flipped+=Flip;
        Player.moves+=AnimateWithPlayer;
    }
    void Flip()
    {
        transform.localScale *= new Vector2(-1,1);
    }
    void AnimateWithPlayer(bool success)
    {   
        anim.SetBool(AnimationStrings.isMoving, success);
    } 

    
}
