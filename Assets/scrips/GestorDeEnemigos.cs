using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorDeEnemigos : MonoBehaviour
{
    public List<Enemigo> enemigos; // Lista de enemigos

    void Start()
    {
        // Asegúrate de que la lista de enemigos esté correctamente llena
        enemigos = new List<Enemigo>(FindObjectsOfType<Enemigo>());
    }

    // Notificar a todos los enemigos que el jugador está escondido
    public void NotificarEscondite(bool estaEscondido)
    {
        foreach (Enemigo enemigo in enemigos)
        {
            enemigo.SetPlayerHiding(estaEscondido); // Llama al método SetPlayerHiding para cada enemigo
        }
    }
}