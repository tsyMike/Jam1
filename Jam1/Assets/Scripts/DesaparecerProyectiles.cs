using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesaparecerProyectiles : MonoBehaviour
{   
    //El tiempo que tarda el objeto en desaparecer
    public float tiempoDeVida=2f;
    public int attackDamage=1;
   private void OnEnable()
    {
        // Inicia la corutina para desactivar el GameObject después de 2 segundos.
        StartCoroutine(DesactivarDespuesDeEspera(tiempoDeVida));
    }

    private IEnumerator DesactivarDespuesDeEspera(float espera)
    {
        // Espera durante el tiempo especificado.
        yield return new WaitForSeconds(espera);
        Pooler.Despawn(gameObject);
        // Desactiva el GameObject después de la espera usando pooling.
        
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
            }
            Pooler.Despawn(gameObject);
        }
    }
}
