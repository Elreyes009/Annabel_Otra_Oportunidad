using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movimiento : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Vector2 gridSize = new Vector2(1f, 1f);
    public LayerMask obstacleLayer;
    private bool isHiding = false;
    private bool isMoving = false;
    private Vector2 input;

    void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Evita movimiento diagonal
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                Vector3 targetPosition = transform.position + new Vector3(input.x * gridSize.x, input.y * gridSize.y, 0);

                // Si la casilla está libre, moverse
                if (!IsObstacle(targetPosition))
                {
                    StartCoroutine(Move(targetPosition));
                }
            }
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
        return obstacle != null; // Si hay un obstáculo, devuelve true
    }

    // Función para verificar si el jugador se está moviendo
    public bool IsPlayerMoving()
    {
        return isMoving;
    }
    public void SetHiding(bool hiding)
    {
        isHiding = hiding;
        FindAnyObjectByType<Enemigo>()?.SetPlayerHiding(hiding);
    }
}