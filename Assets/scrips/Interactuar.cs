using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuar : MonoBehaviour
{
    public float distanciaRay = 2f; // Distancia del raycast
    public LayerMask capaInteractuable; // Capa de objetos interactuables
    private Vector2 direccionMovimiento = Vector2.right; // Dirección por defecto

    void Update()
    {
        // 1️⃣ Actualiza la dirección del Raycast según el input del jugador
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            direccionMovimiento = new Vector2(horizontal, vertical).normalized;
        }

        // 2️⃣ Si el jugador presiona "E", lanza un Raycast en la dirección del movimiento
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direccionMovimiento, distanciaRay, capaInteractuable);

            if (hit.collider != null)
            {


                if (hit.transform.CompareTag("Interactuable"))
                {
                    NPC npc = hit.collider.GetComponent<NPC>();
                    Puerta puerta = hit.collider.GetComponent<Puerta>();



                    if (npc != null)
                    {
                        npc.ActivarDialogo();
                        return;
                    }
                    if (puerta != null)
                    {
                        puerta.AbrirPuerta();
                        return;
                    }
                }

            }
        }
    }

}