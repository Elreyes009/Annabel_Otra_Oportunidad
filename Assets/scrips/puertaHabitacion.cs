using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puertaHabitacion : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena a la que queremos ir

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}