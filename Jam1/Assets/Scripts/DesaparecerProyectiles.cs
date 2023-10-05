using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script para los proyectiles, coloca su tiempo de vida y da;o
public class DesaparecerProyectiles : MonoBehaviour
{   
    //El tiempo que tarda el objeto en desaparecer
    public float tiempoDeVida=2f;
    public int attackDamage=1;
    private Animator anim;
    private void Start() {
        anim= GetComponent<Animator>();
    }
   private void OnEnable()
    {
        // Inicia la corutina para desactivar el GameObject después de 2 segundos.
        StartCoroutine(DesactivarDespuesDeEspera(tiempoDeVida));
    }

    private IEnumerator DesactivarDespuesDeEspera(float espera)
    {
        // Espera durante el tiempo especificado.
        yield return new WaitForSeconds(espera);
        anim.SetTrigger("Dissapear");
        StartCoroutine(Despawn(0.2f));
        // Desactiva el GameObject después de la espera usando pooling.
        
    }
    private IEnumerator Despawn(float espera){
        yield return new WaitForSeconds(espera);
        Pooler.Despawn(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Si colisiona con un objeto con el tag "enemigo", desactiva el GameObject.
            Damageable damageable = collision.GetComponent<Damageable>();
            if(damageable){
                //Debug.Log("Hit"+attackDamage);
                damageable.Hit(attackDamage);
                anim.SetTrigger("Hit");
            }
            StartCoroutine(Despawn(0.3f));
        }
    }
}
