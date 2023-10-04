using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MageBehaviour : MonoBehaviour
{   
    public GameObject prefabProyectil;
    public float velocidadProyectil = 5f;
   


    private void Start() {
        DetectarEnemigos.onEnemyRange+=Lanzar;
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

   
}
