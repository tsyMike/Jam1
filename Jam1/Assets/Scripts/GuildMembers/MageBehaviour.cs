using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MageBehaviour : MonoBehaviour
{   
    public GameObject prefabProyectil;
    public float velocidadProyectil = 5f;
    
    Animator anim;

    private void Start() {
        anim=gameObject.GetComponent<Animator>();
        DetectarEnemigos.onEnemyRange+=Lanzar;
        Player.flipped+=Flip;
        Player.moves+=AnimateWithPlayer;
    }
    

    void Lanzar(Transform objetivo)
    {
        if (prefabProyectil != null && objetivo != null)
        {
            // Instancia el proyectil.
            GameObject proyectil = Pooler.Spawn(prefabProyectil, transform.position, Quaternion.identity);

            // Calcula la dirección hacia el objetivo.
            Vector2 direccion = (objetivo.position - transform.position).normalized;

            // Aplica velocidad al proyectil.
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            rb.velocity = direccion * velocidadProyectil;
        }
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
