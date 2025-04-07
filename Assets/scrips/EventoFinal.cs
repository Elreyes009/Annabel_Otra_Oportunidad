using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoFinal : MonoBehaviour
{
    public Animator libroAnimator; // Referencia al Animator del libro
    public GameObject enemigo; // Enemigo que inicia desactivado

    void Start()
    {
        if (enemigo != null)
        {
            enemigo.SetActive(false); // Asegurar que el enemigo esté desactivado al inicio
        }
    }

    // Método para activar el evento desde el NPC
    public void ActivarEvento()
    {
        if (libroAnimator != null)
        {
            libroAnimator.SetTrigger("Invocar"); // Activar animación del libro
        }

        if (enemigo != null)
        {
            enemigo.SetActive(true); // Activar enemigo
        }
    }
}