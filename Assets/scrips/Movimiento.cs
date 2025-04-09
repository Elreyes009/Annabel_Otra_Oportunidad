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
    private Animator anim;

    private Vector2 lastMovementDirection = Vector2.down;

    void Awake()
    {
        // Asegurarse de que se obtiene el Animator correctamente
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("No se encontró el componente Animator en este GameObject.");
        }
    }

    // Actualizamos la animación cada frame
    void Update()
    {
        MovAnim();
    }

    // Movemos al personaje en FixedUpdate para que la física sea más estable
    void FixedUpdate()
    {
        // Se evita movimiento diagonal
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.x != 0)
            input.y = 0;

        if (!isMoving && input != Vector2.zero)
        {
            Vector3 targetPosition = transform.position + new Vector3(input.x * gridSize.x, input.y * gridSize.y, 0);

            // Si la casilla destino está libre, se procede a mover
            if (!IsObstacle(targetPosition))
            {
                StartCoroutine(Move(targetPosition));
            }
        }
    }

    private void MovAnim()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical);

        // Si no se detecta movimiento, se activa el estado Idle inmediatamente
        if (movement.sqrMagnitude < 0.01f)
        {
            anim.SetInteger("Direccion", 0);
            return;
        }

        // Actualizar la dirección de movimiento
        lastMovementDirection = movement.normalized;

        // Determinar si el movimiento es mayor en horizontal o en vertical
        if (Mathf.Abs(lastMovementDirection.x) > Mathf.Abs(lastMovementDirection.y))
        {
            // Movimiento horizontal
            if (lastMovementDirection.x > 0)
            {
                anim.SetInteger("Direccion", 3); // Derecha
            }
            else
            {
                anim.SetInteger("Direccion", 4); // Izquierda
            }
        }
        else
        {
            // Movimiento vertical
            if (lastMovementDirection.y > 0)
            {
                anim.SetInteger("Direccion", 2); // Arriba
            }
            else
            {
                anim.SetInteger("Direccion", 1); // Abajo
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

        transform.position = targetPosition; // Asegura que termina exactamente en la posición
        isMoving = false;
    }

    bool IsObstacle(Vector3 targetPosition)
    {
        Collider2D obstacle = Physics2D.OverlapCircle(targetPosition, 0.2f, obstacleLayer);
        return obstacle != null;
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
