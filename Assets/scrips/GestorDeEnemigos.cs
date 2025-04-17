using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorDeEnemigos : MonoBehaviour
{
    public List<Enemigo> enemigos; // Lista de enemigos
    


    void Start()
    {
        // Aseg�rate de que la lista de enemigos est� correctamente llena
        enemigos = new List<Enemigo>(FindObjectsOfType<Enemigo>());
    }

    
    
    public void NotificarEscondite(bool estaEscondido)
    {
        foreach (Enemigo enemigo in enemigos)
        {

            enemigo.SetPlayerHiding(estaEscondido); // Llama al m�todo SetPlayerHiding para cada enemigo
        }
    }

    public void VolverAposicionInicial()
    {
        foreach (Enemigo enemigo in enemigos)
        {
            StopAllCoroutines(); // Detiene cualquier movimiento en curso
            enemigo.isMoving = false; // Evita que el enemigo siga movi�ndose
            transform.position = enemigo.posicionInicial; // Vuelve a la posici�n de origen
        }
    }
}