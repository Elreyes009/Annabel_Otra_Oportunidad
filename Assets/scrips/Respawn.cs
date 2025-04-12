//using System.Collections;
//using System.Collections.Generic;
//using Unity.Mathematics;
//using UnityEngine;

//public class Respawn : MonoBehaviour
//{
//    public Transform respawnPoint;
//    private Movimiento playerMovement;
//    private Animator playerAnimator;
//    private SpriteRenderer playerSprite;
//    private Vector3 puntoDeRespawn;
//    private Quaternion rotationRespawn;

//    void Start()
//    {
//        playerMovement = GetComponent<Movimiento>();
//        playerAnimator = GetComponent<Animator>();
//        playerSprite = GetComponent<SpriteRenderer>();
//        puntoDeRespawn = transform.position; // Guarda la posición inicial
//        rotationRespawn = transform.rotation;
        
//    }

//    public void Reaparecer()
//    {
//        StartCoroutine(RespawnCoroutine());
//    }

//    IEnumerator RespawnCoroutine()
//    {
//        playerSprite.enabled = false; // Ocultar al jugador
//        yield return new WaitForSeconds(1f); // Esperar antes de reaparecer
//        transform.position = puntoDeRespawn; // Mover al último checkpoint
//        transform.rotation = rotationRespawn;
//        playerSprite.enabled = true; // Mostrar al jugador
//        playerMovement.enabled = true; // Reactivar el control
//    }

//    public void ActualizarPuntoDeRespawn(Vector3 nuevaPosicion)
//    {
//        puntoDeRespawn = nuevaPosicion; // Guarda el nuevo checkpoint
//    }
//}