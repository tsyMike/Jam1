using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Temporizador : MonoBehaviour
{
    public float tiempoInicial = 180.0f; // Tiempo inicial en segundos
    private float tiempoActual;
    public TMP_Text textoTemporizador; // Asigna el Texto desde el Inspector

    private void Start()
    {
        tiempoActual = tiempoInicial;
    }

    private void Update()
    {
        // Actualiza el tiempo y el Texto
        tiempoActual -= Time.deltaTime;

        if (tiempoActual <= 0)
        {
            tiempoActual = 0;
            // Aquí puedes agregar código para manejar el evento cuando el tiempo se agota
        }

        ActualizarTextoTemporizador();
    }

    private void ActualizarTextoTemporizador()
    {
        int segundos = Mathf.FloorToInt(tiempoActual % 60);
        int minutos = Mathf.FloorToInt(tiempoActual / 60);

        string tiempoTexto = string.Format("{0:00}:{1:00}", minutos, segundos);
        textoTemporizador.text = tiempoTexto;
    }
}
