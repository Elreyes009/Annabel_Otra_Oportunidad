using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mueble : MonoBehaviour
{
    public TextMeshProUGUI dialogoTexto; // Referencia al texto en Unity
    public GameObject dialogoFondo; // Fondo del cuadro de diálogo

    private List<string> dialogos = new List<string>
    {
        "¿Qué es esto? Hay una nota...",
        "La nota dice: \"Ve al colegio\".",
        "No dice quién la dejó...",
        "Esto es raro...",
        "Creo que tomare mi linterna e iré"
    };

    private int indiceTexto = 0;
    private bool playerInRange = false;
    private bool dialogoActivo = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogoActivo)
            {
                SiguienteTexto();
            }
            else
            {
                MostrarTexto();
            }
        }
    }

    void MostrarTexto()
    {
        indiceTexto = 0;
        dialogoTexto.text = dialogos[indiceTexto]; // Muestra el primer texto
        dialogoTexto.gameObject.SetActive(true);
        dialogoFondo.SetActive(true);
        dialogoActivo = true;
    }

    void SiguienteTexto()
    {
        if (indiceTexto < dialogos.Count - 1)
        {
            indiceTexto++;
            dialogoTexto.text = dialogos[indiceTexto]; // Cambia al siguiente texto
        }
        else
        {
            OcultarTexto();
        }
    }

    void OcultarTexto()
    {
        dialogoTexto.gameObject.SetActive(false);
        dialogoFondo.SetActive(false);
        dialogoActivo = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            OcultarTexto();
        }
    }
}