using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escondite : MonoBehaviour
{
    private bool playerInRange = false;
    private bool isHiding = false;
    private SpriteRenderer playerSprite;
    private Movimiento player;
    private GestorDeEnemigos gestorDeEnemigos; // Nueva referencia al GestorDeEnemigos

    void Start()
    {
        player = FindAnyObjectByType<Movimiento>();
        if (player != null)
        {
            playerSprite = player.GetComponent<SpriteRenderer>();
        }

        gestorDeEnemigos = FindAnyObjectByType<GestorDeEnemigos>(); // Encuentra el gestor de enemigos
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isHiding)
            {
                SalirDelEscondite();
            }
            else
            {
                Esconderse();
            }
        }
    }

    void Esconderse()
    {
        isHiding = true;
        playerSprite.enabled = false; // Oculta al jugador
        player.SetHiding(true); // Notifica que está escondido
        player.enabled = false; // Deshabilita el script de movimiento

        // Notifica al GestorDeEnemigos que el jugador está escondido
        if (gestorDeEnemigos != null)
        {
            gestorDeEnemigos.NotificarEscondite(true);
        }
    }

    void SalirDelEscondite()
    {
        isHiding = false;
        playerSprite.enabled = true; // Muestra al jugador
        player.SetHiding(false); // Notifica que dejó de estar escondido
        player.enabled = true; // Habilita el script de movimiento nuevamente

        // Notifica al GestorDeEnemigos que el jugador ha dejado de estar escondido
        if (gestorDeEnemigos != null)
        {
            gestorDeEnemigos.NotificarEscondite(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
