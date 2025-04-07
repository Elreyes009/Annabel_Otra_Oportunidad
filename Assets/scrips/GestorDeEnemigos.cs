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

    // Notificar a todos los enemigos que el jugador est� escondido
    public void NotificarEscondite(bool estaEscondido)
    {
        foreach (Enemigo enemigo in enemigos)
        {
            enemigo.SetPlayerHiding(estaEscondido); // Llama al m�todo SetPlayerHiding para cada enemigo
        }
    }
}