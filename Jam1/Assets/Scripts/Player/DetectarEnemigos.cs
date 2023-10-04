using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarEnemigos : MonoBehaviour
{   
    public GameObject target;
    void Update()
    {
        // Configura un filtro para buscar colliders en la capa "enemigos".
        ContactFilter2D filtro = new ContactFilter2D();
        filtro.SetLayerMask(LayerMask.GetMask("Enemy"));

        // Lista para almacenar los resultados de la detección.
        Collider2D[] resultados = new Collider2D[10]; // Ajusta el tamaño según tus necesidades.

        // Realiza la detección de colisión.
        int numColisiones = Physics2D.OverlapCollider(GetComponent<Collider2D>(), filtro, resultados);

        if (numColisiones > 0)
        {
            // Inicializa la distancia mínima como infinito.
            float distanciaMinima = Mathf.Infinity;
            Collider2D enemigoMasCercano = null;

            // Obtén la posición del objeto actual.
            Vector2 posicionActual = transform.position;

            // Itera a través de los resultados para encontrar el enemigo más cercano.
            for (int i = 0; i < numColisiones; i++)
            {
                Collider2D collider = resultados[i];
                Vector2 posicionEnemigo = collider.transform.position;

                // Calcula la distancia entre el objeto actual y el enemigo actual.
                float distancia = Vector2.Distance(posicionActual, posicionEnemigo);

                // Si esta distancia es menor que la distancia mínima actual, actualiza el enemigo más cercano.
                if (distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    enemigoMasCercano = collider;
                }
            }

            // Ahora 'enemigoMasCercano' contiene el collider del enemigo más cercano.
            if (enemigoMasCercano != null)
            {   
                target=enemigoMasCercano.gameObject;
                Debug.Log("Enemigo En rango");
                // Realiza acciones con el enemigo más cercano, por ejemplo:
                // enemigoMasCercano.gameObject.GetComponent<Enemigo>().RealizarAccion();
            }
        }
    }
}
