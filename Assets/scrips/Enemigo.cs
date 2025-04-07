using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Enemigo : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Vector2 gridSize = new Vector2(1f, 1f);
    public LayerMask obstacleLayer;

    private bool playerIsHiding = false;
    private bool isMoving = false;
    private Transform player;
    private Movimiento playerMovement;
    private Animator playerAnimator;

    private Vector3 posicionInicial;

    void Start()
    {
        player = FindObjectOfType<Movimiento>().transform;
        if (player != null)
        {
            playerMovement = player.GetComponent<Movimiento>();
            playerAnimator = player.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("No se encontró el objeto del jugador en la escena.");
        }

        posicionInicial = transform.position;
    }

    void Update()
    {
        if (playerMovement != null && playerMovement.IsPlayerMoving() && !isMoving && !playerIsHiding)
        {
            MoverEnemigo();
        }
    }

    public void MoverEnemigo()
    {
        if (isMoving || player == null) return;

        Vector3 playerPosition = player.position;
        Vector3 enemyPosition = transform.position;
        Vector3 direction = (playerPosition - enemyPosition).normalized;

        Vector2 moveDirection = Vector2.zero;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            moveDirection = new Vector2(Mathf.Sign(direction.x), 0);
        }
        else
        {
            moveDirection = new Vector2(0, Mathf.Sign(direction.y));
        }

        Vector3 targetPosition = enemyPosition + new Vector3(moveDirection.x * gridSize.x, moveDirection.y * gridSize.y, 0);

        if (!IsObstacle(targetPosition))
        {
            StartCoroutine(Move(targetPosition));
        }
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
    }

    bool IsObstacle(Vector3 targetPosition)
    {
        Collider2D obstacle = Physics2D.OverlapCircle(targetPosition, 0.2f, obstacleLayer);
        return obstacle != null;
    }

    public void SetPlayerHiding(bool isHiding)
    {
        playerIsHiding = isHiding;
        if (playerIsHiding)
        {
            VolverAposicionInicial();
        }
    }

    private void VolverAposicionInicial()
    {
        StopAllCoroutines(); // Detiene cualquier movimiento en curso
        isMoving = false; // Evita que el enemigo siga moviéndose
        transform.position = posicionInicial; // Vuelve a la posición de origen
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MatarJugador());
        }
    }

    IEnumerator MatarJugador()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        else
        {
            Debug.LogError("Movimiento del jugador no encontrado.");
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Muerte");
        }
        else
        {
            Debug.LogError("Animator del jugador no encontrado.");
        }

        yield return new WaitForSeconds(2f);

        Respawn respawnScript = player.GetComponent<Respawn>();
        if (respawnScript != null)
        {
            respawnScript.Reaparecer();
        }
        else
        {
            Debug.LogError("El script Respawn no está asignado al jugador.");
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        VolverAposicionInicial();
    }
}
