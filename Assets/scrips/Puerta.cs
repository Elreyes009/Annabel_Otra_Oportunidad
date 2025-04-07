using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    private bool abierta = false;
    private Animator animator;
    private Collider2D colisionador;
    private Rigidbody2D rb;

    public string objetoRequerido = "Llave"; // Nombre del objeto necesario para abrir la puerta

    void Start()
    {
        animator = GetComponent<Animator>();
        colisionador = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void AbrirPuerta()
    {
        if (!abierta && Inventario.instance.TieneObjeto(objetoRequerido)) // 🔹 Verifica si el jugador tiene la llave
        {
            abierta = true;
            animator.SetTrigger("Abrir");
            colisionador.enabled = false;
            rb.simulated = false;

            // Rompe la llave y la elimina del inventario
            Inventario.instance.UsarLlave(objetoRequerido);
        }
        else
        {
            Debug.Log("No tienes la " + objetoRequerido + " para abrir esta puerta."); // Mensaje en consola
        }
    }
}